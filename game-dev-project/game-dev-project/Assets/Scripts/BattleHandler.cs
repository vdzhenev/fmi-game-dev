using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    //Prefabs for Player Characters and Enemies
    [SerializeField] private Transform[] pfPlayerCharacters;
    [SerializeField] private Transform[] pfEnemyCharacters;
    [SerializeField] private GameObject AbilityLayout;
    [SerializeField] private GameObject TrackerUI;
    
    private enum State 
    {
        WaitingForPlayer,
        Busy,
        WON,
        LOST
    }

    private const int MAX_PLAYER_TEAM_SIZE = 4;
    private const int MAX_ENEMY_TEAM_SIZE = 6;
    private const int TOTAL_SIZE = MAX_PLAYER_TEAM_SIZE + MAX_ENEMY_TEAM_SIZE;

    private List<Transform> initiativeCount;
    private List<Transform> playerTeam;
    private List<Transform> enemyTeam;

    private int currTurn = -1;
    private Transform currentPlayer;

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        initiativeCount = new List<Transform>();
        playerTeam = new List<Transform>();
        enemyTeam = new List<Transform>();
        state = State.Busy;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        for(int i = 0; i < MAX_PLAYER_TEAM_SIZE; ++i)
        {
            SpawnCharacter(true, i);
        }
        for(int i = 0; i < MAX_ENEMY_TEAM_SIZE; ++i)
        {
            SpawnCharacter(false, i);
        }

        initiativeCount.Sort(SortByInitiative);

        TrackerUI.GetComponent<UpdateTracker>().Setup(initiativeCount, TOTAL_SIZE);

        ChooseNextCharacter();

        yield return new WaitForSeconds(2f);
        
    }

    // Places characters and enemies on the field
    private void SpawnCharacter(bool isOnPlayerTeam, int place)
    {
        Vector3 position;
        if(isOnPlayerTeam)
        {
            if(place <MAX_PLAYER_TEAM_SIZE)
            {
                position = new Vector3(-2+(place%2),(place<2?1:-1), -1);
                Transform curr = Instantiate(pfPlayerCharacters[place], position, Quaternion.identity);
                curr.name = pfPlayerCharacters[place].name;
                curr.GetComponent<CharacterStat>().startBattle();
                curr.GetComponent<CharacterStat>().ID = place;
                initiativeCount.Add(curr);
                playerTeam.Add(curr);
            }
        }
        else if(place <MAX_ENEMY_TEAM_SIZE)
        {
            position = new Vector3(1+(place%2), (place < 2 ? 1:(place <4 ? 0 : -1) ), -1);
            Transform curr = Instantiate(pfEnemyCharacters[place], position, Quaternion.identity);
            curr.name = "Enemy" + place;
            curr.GetComponent<CharacterStat>().startBattle();
            curr.GetComponent<CharacterStat>().ID = MAX_PLAYER_TEAM_SIZE + place;
            initiativeCount.Add(curr);
            enemyTeam.Add(curr);
        }

    }

    IEnumerator EnemyTurn()
    {
        if(state==State.Busy)
        {     
            yield return new WaitForSeconds(2f);
            int targetNum = Random.Range(0, MAX_PLAYER_TEAM_SIZE-1);
            Transform target = playerTeam[targetNum];
            while(target.GetComponent<CharacterStat>().isDead() && !checkBattleLost())
            {
                Debug.Log(target.name + " is dead. Choosing next...");
                ++targetNum;
                target = playerTeam[targetNum%MAX_PLAYER_TEAM_SIZE];
                Debug.Log(targetNum);
            }
            currentPlayer.GetComponent<CharacterStat>().useAbility(0, target);

            //yield return new WaitForSeconds(1f);
            ChooseNextCharacter();
        }
    }

    void PlayerTurn()
    {
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        if(CS.isDead() || CS.numOfActions <= 0)
        {
            state = State.Busy;
            CS.refreshActions();
            ChooseNextCharacter();
        }

    }

    public void OnAbility(int N)
    {
        if(state != State.WaitingForPlayer)
            return;
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        if(CS.abilities[N].getUses()==0)
            return;
        Ability.Target allowed = CS.abilities[N].getTarget();

        switch(allowed)
        {
            case Ability.Target.Self:
                CS.useAbility(N, currentPlayer);
                break;
            case Ability.Target.SingleEnemy:
                CS.useAbility(N, enemyTeam[Random.Range(0, MAX_PLAYER_TEAM_SIZE-1)]);
                break;
            case Ability.Target.EnemyBack:
                List<Transform> backCol = new List<Transform>();
                for(int i = 1; i<MAX_ENEMY_TEAM_SIZE; i+=2)
                {
                    backCol.Add(enemyTeam[i]);
                }
                CS.useAbility(N, backCol);
                break;
            case Ability.Target.EnemyFront:
                List<Transform> frontCol = new List<Transform>();
                for(int i = 0; i<MAX_ENEMY_TEAM_SIZE; i+=2)
                {
                    frontCol.Add(enemyTeam[i]);
                }
                CS.useAbility(N, frontCol);
                break;
            case Ability.Target.SingleAlly:
                CS.useAbility(N, playerTeam[Random.Range(0, MAX_PLAYER_TEAM_SIZE-1)]);
                break;
            case Ability.Target.AllAllies:
                CS.useAbility(N, playerTeam);
                break;
            default:
                Debug.Log("Error while choosing target!");
                break;
        }
        
        Debug.Log("Used ab " + N);
        PlayerTurn();
    }

    private void ChooseNextCharacter()
    {
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
            ++currTurn;
            currentPlayer = initiativeCount[currTurn%TOTAL_SIZE];
            TrackerUI.GetComponent<UpdateTracker>().UpdateUI();
            //Debug.Log(currentPlayer.name + "\'s turn");
            if(currentPlayer.GetComponent<CharacterStat>().currHP <= 0)
            {
                ChooseNextCharacter();
            }
            if(currentPlayer.CompareTag("Player"))
            {
                state = State.WaitingForPlayer;
                AbilityLayout.GetComponent<UpdateAbilityUI>().updateAbilities(currentPlayer);
                PlayerTurn();
            }
            else
            {
                state = State.Busy;
                StartCoroutine(EnemyTurn());
            }
        }
    }

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

    private void loseBattle()
    {
        Debug.Log("You lost the battle :(");
    }
    
    private void winBattle()
    {
        Debug.Log("You won the battle :)");
    }


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
