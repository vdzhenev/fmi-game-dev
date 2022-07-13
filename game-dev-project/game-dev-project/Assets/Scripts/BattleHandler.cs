using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHandler : MonoBehaviour
{
    private static BattleHandler instance;

    //Prefabs for Player Characters and Enemies
    [SerializeField] private Transform[] pfPlayerCharacters;
    [SerializeField] private Transform[] pfEnemyCharacters;
    [SerializeField] private GameObject AbilityLayout;
    [SerializeField] private GameObject TrackerUI;
    [SerializeField] private GameObject CharacterStatDisplay;
    [SerializeField] private Canvas BattleOverCanvas;

    //Positions at which characters are spawned
    [SerializeField] private Transform[] PlayerPositions;
    [SerializeField] private Transform[] EnemyPositions;
    
    //enum of the different states of the game - either player turn, CPU turn or end battle state
    private enum State 
    {
        WaitingForPlayer,
        Busy,
        WON,
        LOST
    }

    //Team sizes on both the enemy and the player side
    private int MAX_PLAYER_TEAM_SIZE;
    private int MAX_ENEMY_TEAM_SIZE;
    private int TOTAL_SIZE;

    //List that keeps track of the Initiative - order at which characters take turns
    private List<Transform> initiativeCount;

    //Lists that keep track of characters on the player team and on the enemy team
    private static List<Transform> playerTeam;
    private List<Transform> enemyTeam;

    //Current turn number and character
    private int currTurn = -1;
    private Transform currentPlayer;
    //Selected character - changed by using Left Mouse Button
    private Transform selected;
    private Camera mainCam;

    //State of the game
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        MAX_PLAYER_TEAM_SIZE = PlayerPositions.Length;
        MAX_ENEMY_TEAM_SIZE = EnemyPositions.Length;
        TOTAL_SIZE = MAX_PLAYER_TEAM_SIZE + MAX_ENEMY_TEAM_SIZE;
        selected = null;
        mainCam = Camera.main;
        BattleOverCanvas.GetComponent<BattleOver>().Hide();
        initiativeCount = new List<Transform>();
        playerTeam = new List<Transform>();
        enemyTeam = new List<Transform>();
        state = State.Busy;
        StartCoroutine(SetupBattle());
    }

    private void Update() 
    {
        ClickTarget();
    }

    //Checks for mouse input and casts a ray from mouse position to world position
    bool ClickTarget()
    {
        if(Input.GetMouseButton(0))
        {
            //Checks if ray hit a "clickable"
            RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity, LayerMask.GetMask("Clickable"));
            if(hit.collider != null)
            {
                //Deselects previous selected entity
                if(selected) selected.GetComponent<SpriteRenderer>().color = Color.white;

                //Selects the new one and changes its tint to a slightly darker one
                selected = hit.transform;
                selected.GetComponent<SpriteRenderer>().color = Color.gray;
                //Display the stats of the selected entity
                CharacterStatDisplay.GetComponent<StatDisplay>().updateStatDisplay(selected);
                return true;
            }
            else
            {
                //If the click didn't result in a valid selection it displays the stats of the current player
                CharacterStatDisplay.GetComponent<StatDisplay>().updateStatDisplay(currentPlayer);
            }
        }
        return false;
    }

    //Run at the start of battle - sets up the battlefield and instantiates the characters from their prefabs
    IEnumerator SetupBattle()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length == 0) 
        {
            for(int i = 0; i < MAX_PLAYER_TEAM_SIZE; ++i)
            {
                SpawnCharacter(true, i);
            }
        }
        else
        {
            foreach(GameObject p in players) 
            {
                p.transform.GetComponent<CharacterStat>().startBattle();
                initiativeCount.Add(p.transform);
                playerTeam.Add(p.transform);
            }
        }
        for(int i = 0; i < MAX_ENEMY_TEAM_SIZE; ++i)
        {
            SpawnCharacter(false, i);
        }

        //Sorts the Initiative list in descending order
        initiativeCount.Sort(SortByInitiative);
        //Updates the tracker UI
        TrackerUI.GetComponent<UpdateTracker>().Setup(initiativeCount, TOTAL_SIZE);

        //Decides who the next character should be and waits for 2 seconds
        ChooseNextCharacter();

        yield return new WaitForSeconds(2f);
        
    }

    // Places characters and enemies on the field
    private void SpawnCharacter(bool isOnPlayerTeam, int place)
    {
        //Vector3 position;
        if(isOnPlayerTeam)
        {
            if(place <MAX_PLAYER_TEAM_SIZE)
            {
                //Position is dependant on the index of the character, placing them in order from top left to bottom right
                //position = new Vector3(-2+(place%2),(place<2?1:-1), -1);
                Transform curr = Instantiate(pfPlayerCharacters[place], PlayerPositions[place].position, Quaternion.identity);
                curr.name = pfPlayerCharacters[place].name;
                //Initial setup for each character
                curr.GetComponent<CharacterStat>().startBattle();
                //Adds character to initative tracker and player team tracker
                initiativeCount.Add(curr);
                playerTeam.Add(curr);
            }
        }
        else if(place <MAX_ENEMY_TEAM_SIZE)
        {
            //position = new Vector3(1+(place%2), (place < 2 ? 1:(place <4 ? 0 : -1) ), -1);
            Transform curr = Instantiate(pfEnemyCharacters[place], EnemyPositions[place].position, Quaternion.identity);
            curr.name = pfEnemyCharacters[place].name;
            curr.GetComponent<CharacterStat>().startBattle();
            initiativeCount.Add(curr);
            enemyTeam.Add(curr);
        }

    }

    //Sample enemy turn
    IEnumerator EnemyTurn()
    {
        CharacterStat cs = currentPlayer.GetComponent<CharacterStat>();
        Animator animator = currentPlayer.GetComponent<Animator>();
        if(state==State.Busy)
        {     
            yield return new WaitForSeconds(2f);
            animator.SetTrigger("StartTurn");
            //Enemy needs to have available actions
            if(cs.numOfActions > 0)
            {
                int cycles = 0;
                //Chooses a random (targetable) character on the player team and attacks them
                int targetNum = Random.Range(0, MAX_PLAYER_TEAM_SIZE-1);
                Transform target = playerTeam[targetNum];
                while(cycles <MAX_PLAYER_TEAM_SIZE && !target.GetComponent<CharacterStat>().canBeTargeted && !checkBattleLost())
                {
                    Debug.Log(target.name + " is dead. Choosing next...");
                    ++targetNum;
                    ++cycles;
                    target = playerTeam[targetNum%MAX_PLAYER_TEAM_SIZE];
                    Debug.Log(targetNum);
                }
                if(cycles<MAX_PLAYER_TEAM_SIZE)
                    cs.useAbility(0, target);
                else 
                    Debug.Log("Can't find valid target.");
            }
            animator.SetTrigger("EndTurn");
            cs.refreshActions();
            //yield return new WaitForSeconds(1f);
            ChooseNextCharacter();
        }
    }

    //Player turn method
    void PlayerTurn()
    {
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        Animator animator = currentPlayer.GetComponent<Animator>();
        //If the player is dead or has no available actions, the turn ends
        if(CS.isDead() || CS.numOfActions <= 0)
        {
            state = State.Busy;
            CS.refreshActions();
            animator.SetTrigger("EndTurn");
            ChooseNextCharacter();
            return;
        }
        animator.SetTrigger("StartTurn");
    }

    //Method called on ability button click
    public void OnAbility(int N)
    {
        if(state != State.WaitingForPlayer)
            return;
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        //Abilities can have limited uses each battle
        if(CS.abilities[N].getUses()==0)
        {
            CS.Speak("I can't use that right now.");
            //TextPopup.Create(currentPlayer.position, "I can't use this right now!");
            SoundManager.PlaySound(SoundManager.Sound.Error);
            return;
        }
        //Uses the ability based on what its allowed target can be
        Ability.Target allowed = CS.abilities[N].getTarget();
        switch(allowed)
        {
            //Self = current player
            case Ability.Target.Self:
                CS.useAbility(N, currentPlayer);
                break;
            //SingleEnemy = selected character must be on enemy team
            case Ability.Target.SingleEnemy:
                if(selected.GetComponent<CharacterStat>().canBeTargeted && enemyTeam.Contains(selected))
                {
                    CS.useAbility(N, selected);
                }
                else
                {
                    Debug.Log("Incorrect Target.");
                    CS.Speak("That is not a valid target!");
                    SoundManager.PlaySound(SoundManager.Sound.Error);
                    return;
                }
                break;
            //EnemyBack = each character in the back column of the enemy team
            case Ability.Target.EnemyBack:
                List<Transform> backCol = new List<Transform>();
                for(int i = 1; i<MAX_ENEMY_TEAM_SIZE; i+=2)
                {
                    backCol.Add(enemyTeam[i]);
                }
                CS.useAbility(N, backCol);
                break;
            //EnemyFront = each character in the front column of the enemy team
            case Ability.Target.EnemyFront:
                List<Transform> frontCol = new List<Transform>();
                for(int i = 0; i<MAX_ENEMY_TEAM_SIZE; i+=2)
                {
                    frontCol.Add(enemyTeam[i]);
                }
                CS.useAbility(N, frontCol);
                break;
            //SingleEnemy = selected character must be on player team
            case Ability.Target.SingleAlly:
                if(playerTeam.Contains(selected))
                {
                    CS.useAbility(N, selected);
                }
                else
                {
                    Debug.Log("Incorrect Target.");
                    CS.Speak("That is not a valid target!");
                    //TextPopup.Create(currentPlayer.position, "That is not a valid target!");
                    SoundManager.PlaySound(SoundManager.Sound.Error);
                    return;
                }
                break;
            //AllAllies = each character on the player team
            case Ability.Target.AllAllies:
                CS.useAbility(N, playerTeam);
                break;
            default:
                Debug.Log("Error while choosing target!");
                SoundManager.PlaySound(SoundManager.Sound.Error);
                break;
        }
        CharacterStatDisplay.GetComponent<StatDisplay>().updateStatDisplay(currentPlayer);
        //After using an ability, the player may still have actions left, so we go back to check that by calling the function again
        PlayerTurn();
    }

    //Function to choose the next character from the Initiative
    private void ChooseNextCharacter()
    {
        //If the battle is won or lost, the game ends
        if(checkBattleLost())
        {
            state = State.LOST;
            loseBattle();
        }
        else if(checkBattleWon())
        {
            state = State.WON;
            winBattle();
        }
        else
        {
            //Update turn count and tracker UI
            ++currTurn;
            currentPlayer = initiativeCount[currTurn%TOTAL_SIZE];
            TrackerUI.GetComponent<UpdateTracker>().UpdateUI();

            //Skip dead characters
            if(currentPlayer.GetComponent<CharacterStat>().isDead())
            {
                ChooseNextCharacter();
                return;
            }
            //Current character is on player team
            if(currentPlayer.CompareTag("Player"))
            {
                state = State.WaitingForPlayer;
                AbilityLayout.GetComponent<UpdateAbilityUI>().makeActive();
                AbilityLayout.GetComponent<UpdateAbilityUI>().updateAbilities(currentPlayer);
                CharacterStatDisplay.GetComponent<StatDisplay>().updateStatDisplay(currentPlayer);
                currentPlayer.GetComponent<CharacterStat>().tickTimedBuffs();
                PlayerTurn();
            }
            //Current character is not on player team
            else
            {
                AbilityLayout.GetComponent<UpdateAbilityUI>().makeInactive();
                state = State.Busy;
                currentPlayer.GetComponent<CharacterStat>().tickTimedBuffs();
                StartCoroutine(EnemyTurn());
            }
        }
    }

    //Battle is lost when each character on the player team is dead
    private bool checkBattleLost()
    {
        for(int i = 0; i< MAX_PLAYER_TEAM_SIZE; ++i)
        {
            if(!playerTeam[i].GetComponent<CharacterStat>().isDead())
            {
                return false;
            }
        }
        return true;
    }

    //Battle is won when each character on the enemy team is dead
    private bool checkBattleWon()
    {
        for(int i = 0; i< MAX_ENEMY_TEAM_SIZE; ++i)
        {
            if(!enemyTeam[i].GetComponent<CharacterStat>().isDead())
            {
                return false;
            }
        }
        return true;
    }

    //Placeholder functions for winning and losing the battle
    private void loseBattle()
    {
        Debug.Log("You lost the battle :(");
        BattleOverCanvas.GetComponent<BattleOver>().Show(false);
    }
    
    private void winBattle()
    {
        Debug.Log("You won the battle :)");
        BattleOverCanvas.GetComponent<BattleOver>().Show(true);
    }

    //Method that compares two characters, based on their initiative roll
    //Used for sorting initiativeCount in descending order
    //In case of equal initative rolls, the character with a higher DEX stat gets to go first
    static int SortByInitiative(Transform c1, Transform c2)
    {
        CharacterStat c1CS = c1.GetComponent<CharacterStat>();
        CharacterStat c2CS = c2.GetComponent<CharacterStat>();
        if (c1CS.initiative>c2CS.initiative)
        {
            return -1;
        }
        else if (c1CS.initiative<c2CS.initiative)
        {
            return 1;
        }
        else
        {
            if(c1CS.DEX.GetValue()>c2CS.DEX.GetValue())
            {
                return -1;
            }
            else if (c1CS.DEX.GetValue()<c2CS.DEX.GetValue())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
    }
}
