using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Base class used for character stats
public class CharacterStat : MonoBehaviour
{
    //public int ID = -1;

    //Health Points
    public int maxHP = 10;
    public int currHP {get; private set;}

    //Number of actions allowed per turn
    public int baseNumOfActions = 1;
    public int numOfActions {get; private set;}

    //Number between [1+DEX, 20+DEX] which decides the order at which characters get their turn (highest number goes first)
    public int initiative = 0;

    //Stats
    //AC (Armor Class) - Reduces incoming damage
    //STR (Strength) - Increases damage of strength-based attacks and abilities
    //DEX (Dexterity) - Increases damage of dexterity-based attacks and abilites; gets added to initiative rolls
    //WIS (Wisdom) - Increases damage and potency of wisdom-based attacks and abilities
    public Stat AC, STR, DEX, WIS;
    [SerializeField] public List<Ability> abilities;

    //CRT (Critical chance) - % chance at which an attack deals double damage
    //ACC (Accuracy) - % chance to hit with an attack
    public Stat CRT, ACC;

    //List of buffs / debuffs that affect the character
    public List<Buff> buffs;

    //Icons used in the initiative tracker; First Icon is the Big one, second is the small one
    public List<Sprite> Icons;

    public HpBar hpBar;

    
    public Transform myTarget {get; set;}

    protected virtual void Awake() 
    {
        //Set stats to their base values
        AC.setToBase();
        STR.setToBase();
        DEX.setToBase();
        WIS.setToBase();
        CRT.setToBase();
        ACC.setToBase();
        currHP = maxHP;
        buffs = new List<Buff>();
        refreshActions();
        foreach(Ability a in abilities)
        {
            a.refreshUses();
        }
        hpBar.SetMaxHealth(maxHP);
    }

    //Method used for printing ability details
    public void printAbilities()
    {
        foreach(Ability a in abilities)
        {
            Debug.Log(a.getAbilityText());
            Debug.Log(a.getVal());
        }
    }

    //Method used when character takes damage. Takes the value as a parameter
    public void takeDamage(int damage)
    {
        //Damage gets reduced based on the AC of the character
        int finDMG = damage-(Mathf.FloorToInt((AC.GetValue()/10f)*damage));
        currHP -= finDMG;
        Debug.Log(name + " took " + finDMG + " damage\nCurr HP " + currHP);
        //If character is reduced bellow 0 HP, it dies
        if(currHP<=0)
        {
            currHP = 0;
            Die();
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Hit);
        }
        hpBar.SetHealth(currHP);
    }
    
    //Placeholder function for character death
    public void Die()
    {
        Debug.Log(name + " died");
        GetComponent<SpriteRenderer>().enabled = false;
        SoundManager.PlaySound(SoundManager.Sound.Death);
    }

    //Method used when character is healed
    public void heal(int amount)
    {
        //Character must be alive
        if(!isDead())
        {
            currHP+=amount;
            //Can't get healed above maxHP
            if(currHP>maxHP)
            {
                currHP = maxHP;
            }
        }
        SoundManager.PlaySound(SoundManager.Sound.Heal);
        hpBar.SetHealth(currHP);
    }

    //Gets a random number between 1 and 20 and adds character's DEX for initiative
    public void rollInitiative()
    {
        initiative = Random.Range(1, 20) + DEX.GetValue();
        //Debug.Log(name + "rolled a " + initiative);
    }

    //When the battle ends the actions and inititative get refreshed
    public void endBattle()
    {
        refreshActions();
        initiative = 0;
    }

    //When the battle starts each character rolls for initiative and gets its actions refreshed
    public void startBattle()
    {
        refreshActions();
        rollInitiative();
    }
    
    //Number of actions gets set to base value
    public void refreshActions()
    {
        numOfActions = baseNumOfActions;
    }

    //Taking an action reduces number of available actions by 1
    public void takeAction()
    {
        if(numOfActions>0)
        {
            --numOfActions;
        }
        else
        {
            Debug.Log("No actions left!");
        }
    }

    //Method for using a character's ability on a single target. Takes index n and target. Using an ability takes an action.
    public void useAbility(int n, Transform TARGET)
    {
        abilities[n].Use(TARGET);
        takeAction();
    }

    //Method for using a character's ability on multiple targets.
    public void useAbility(int n, List<Transform> TARGET)
    {
        //abilities[n].Use(TARGET);
        StartCoroutine(SlowUse(n, TARGET));
        takeAction();
    }

    IEnumerator SlowUse(int n, List<Transform> Targets)
    {
        bool decrUses = true;
        Debug.Log(Targets.Count);
        foreach(Transform t in Targets)
        {
            abilities[n].Use(t, decrUses);
            yield return new WaitForSeconds(0.2f);
            decrUses = false;
        }
    }

    //Checks if character is dead
    public bool isDead()
    {
        return currHP <= 0;
    }

    //Adds a buff to the list of buffs
    public void addBuff(Buff buff)
    {
        buffs.Add(buff);
        //Debug.Log("Added buff");
    } 

    //"Ticks" each timed buff in the list (buff applies its effects and gets its duration reduced)
    public void tickBuffs()
    {
        foreach(Buff b in buffs.ToArray())
        {
            //Debug.Log("tick");
            b.Tick();
            if(b.isFinished)
            {
                buffs.Remove(b);
            }
        }
    }
}
