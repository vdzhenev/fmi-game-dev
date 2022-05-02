using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    //Prefabs for Player Characters and Enemies
    [SerializeField] private Transform[] pfPlayerCharacters;
    [SerializeField] private Transform[] pfEnemyCharacters;
    [SerializeField] private GameObject AbilityLayout;
    
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
        // currentPlayer = initiativeCount[currTurn];
        // Debug.Log(currentPlayer.name + "\'s turn");
        

        yield return new WaitForSeconds(2f);

        ChooseNextCharacter();
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
            curr.GetComponent<CharacterStat>().startBattle();
            initiativeCount.Add(curr);
            enemyTeam.Add(curr);
        }

    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        currentPlayer.GetComponent<SampleEnemy>().Attack(playerTeam[Random.Range(0, MAX_PLAYER_TEAM_SIZE-1)]);

        yield return new WaitForSeconds(1f);
        ChooseNextCharacter();
    }

    void PlayerTurn()
    {
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        if(CS.currHP<=0 || CS.numOfActions <= 0)
        {
            state = State.Busy;
            CS.refreshActions();
            ChooseNextCharacter();
        }

    }

    public void OnAbility1()
    {
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        CS.takeAction();
        Debug.Log("Used ab 1");
        PlayerTurn();
    }

    public void OnAbility2()
    {
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        CS.takeAction();
        Debug.Log("Used ab 2");
        PlayerTurn();
    }

    public void OnAbility3()
    {
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        CS.takeAction();
        Debug.Log("Used ab 3");
        PlayerTurn();
    }

    public void OnAbility4()
    {
        CharacterStat CS = currentPlayer.GetComponent<CharacterStat>();
        CS.takeAction();
        Debug.Log("Used ab 4");
        PlayerTurn();
    }

    

    private void Update() 
    {
        // if(currentPlayer.CompareTag("Player"))
        // {
        //     state = State.WaitingForPlayer;
        // }
        // if(Input.GetKey(KeyCode.Q) && state == State.WaitingForPlayer)
        // {
        //     state = State.Busy;
        //     Debug.Log(currentPlayer.name + " used ability 1");
        //     ChooseNextCharacter();
        // }
    }

    private void ChooseNextCharacter()
    {
        ++currTurn;
        currentPlayer = initiativeCount[currTurn%TOTAL_SIZE];
        Debug.Log(currentPlayer.name + "\'s turn");
        if(currentPlayer.GetComponent<CharacterStat>().currHP <= 0)
        {
            ChooseNextCharacter();
        }
        if(currentPlayer.CompareTag("Player"))
        {
            state = State.WaitingForPlayer;
            AbilityLayout.GetComponent<UpdateUI>().updateAbilities(currentPlayer.GetComponent<CharacterStat>().ID);
            PlayerTurn();
        }
        else
        {
            state = State.Busy;
            StartCoroutine(EnemyTurn());
        }
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
