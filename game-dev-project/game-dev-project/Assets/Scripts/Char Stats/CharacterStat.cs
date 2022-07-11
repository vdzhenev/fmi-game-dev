using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Base class used for character stats
public class CharacterStat : MonoBehaviour
{
    public bool canBeTargeted;

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

    //Lists of buffs / debuffs that affect the character
    private List<Buff> timedBuffs;          //Timed buffs "tick" at the start of the character's turn
    public List<Buff> onAttackBuffs;       //onAttack buffs tick whenever the character attacks
    private List<Buff> onTakeDamageBuffs;   //onTakeDamage buffs tick whenever the character takes damage

    //Icons used in the initiative tracker; First Icon is the Big one, second is the small one
    public List<Sprite> Icons;

    public HpBar hpBar;
    public BuffBar buffBar;
    public TextPopup textPopup;

    
    public Transform myTarget {get; set;}

    protected virtual void Awake() 
    {
        //Set stats to their base values
        canBeTargeted = true;
        AC.setToBase();
        STR.setToBase();
        DEX.setToBase();
        WIS.setToBase();
        CRT.setToBase();
        ACC.setToBase();
        currHP = maxHP;
        timedBuffs = new List<Buff>();
        onAttackBuffs = new List<Buff>();
        onTakeDamageBuffs = new List<Buff>();
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
    public void takeDamage(int damage, bool crt)
    {
        //Damage gets reduced based on the AC of the character
        int finDMG = damage-(Mathf.FloorToInt((AC.GetValue()/10f)*damage));


        currHP -= finDMG;
        Debug.Log(name + " took " + finDMG + " damage\nCurr HP " + currHP);
        DamagePopup.Create(transform.position, finDMG, crt);
        tickOnTakeDamageBuffs();
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
        canBeTargeted = false;
        removeAllBuffs();
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
        abilities[n].PlayAnimation(transform.position, TARGET.position);
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
            abilities[n].PlayAnimation(transform.position, t.position);
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
    public void addTimedBuff(Buff buff)
    {
        timedBuffs.Add(buff);
        buff.Activate();
        buffBar.AddBuff(buff);
        //Debug.Log("Added buff");
    } 

    public void addOnAttackBuff(Buff buff)
    {
        onAttackBuffs.Add(buff);
        buff.Activate();
        buffBar.AddBuff(buff);
        //Debug.Log("Added buff");
    }

    public void addOnTakeDamageBuff(Buff buff)
    {
        onTakeDamageBuffs.Add(buff);
        buff.Activate();
        buffBar.AddBuff(buff);
    }

    //"Ticks" each timed buff in the list (buff applies its effects and gets its duration reduced)
    public void tickTimedBuffs()
    {
        foreach(Buff b in timedBuffs.ToArray())
        {
            //Debug.Log("tick");
            b.Tick();
            if(b.isFinished)
            {
                timedBuffs.Remove(b);
                buffBar.RemoveBuff(b.type);
            }
        }
    }

    public void tickOnTakeDamageBuffs()
    {
        foreach(Buff b in onTakeDamageBuffs.ToArray())
        {
            //Debug.Log("tick");
            b.Tick();
            if(b.isFinished)
            {
                onTakeDamageBuffs.Remove(b);
                buffBar.RemoveBuff(b.type);
            }
        }
    }

    public void tickOnAttackBuffs()
    {
        foreach(Buff b in onAttackBuffs.ToArray())
        {
            //Debug.Log("tick");
            b.Tick();
            if(b.isFinished)
            {
                onAttackBuffs.Remove(b);
                buffBar.RemoveBuff(b.type);
            }
        }
    }

    private void removeAllBuffs()
    {
        foreach(Buff b in timedBuffs.ToArray())
        {
            b.isFinished = true;    
            timedBuffs.Remove(b);
            buffBar.RemoveBuff(b.type);
        }

        foreach(Buff b in onAttackBuffs.ToArray())
        {
            b.isFinished = true;    
            onAttackBuffs.Remove(b);
            buffBar.RemoveBuff(b.type);
        }

        foreach(Buff b in onTakeDamageBuffs.ToArray())
        {
            b.isFinished = true;    
            onTakeDamageBuffs.Remove(b);
            buffBar.RemoveBuff(b.type);
        }
    }

    public void Speak(string text) 
    {
        textPopup.Setup(text);
    }
}
