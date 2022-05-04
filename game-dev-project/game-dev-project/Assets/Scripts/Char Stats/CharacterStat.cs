using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int ID = -1;

    public int maxHP = 10;
    public int currHP {get; private set;}

    public int baseNumOfActions = 1;
    public int numOfActions {get; private set;}

    public int initiative = 0;

    public Stat AC, STR, DEX, WIS;
    [SerializeField] public List<Ability> abilities;

    
    public Transform myTarget {get; set;}

    protected virtual void Awake() 
    {
        currHP = maxHP;
        refreshActions();
        foreach(Ability a in abilities)
        {
            a.refreshUses();
        }
    }

    public void printAbilities()
    {
        foreach(Ability a in abilities)
        {
            Debug.Log(a.getAbilityText());
            Debug.Log(a.getVal());
        }
    }

    public void takeDamage(int damage)
    {
        int finDMG = damage-(Mathf.FloorToInt((AC.GetValue()/10f)*damage));
        currHP -= finDMG;
        Debug.Log(name + "took " + finDMG + " damage\nCurr HP " + currHP);
        if(currHP<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("character died");
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void heal(int amount)
    {
        if(!isDead())
        {
            Debug.Log("character healed for " + amount);
            currHP+=amount;
            if(currHP>maxHP)
            {
                currHP = maxHP;
            }
        }
    }

    public void rollInitiative()
    {
        initiative = Random.Range(1, 20) + DEX.GetValue();
        //Debug.Log(name + "rolled a " + initiative);
    }

    public void endBattle()
    {
        refreshActions();
        initiative = 0;
    }

    public void startBattle()
    {
        refreshActions();
        rollInitiative();
    }
    
    public void refreshActions()
    {
        numOfActions = baseNumOfActions;
    }

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

    public void useAbility(int n, Transform TARGET)
    {
        abilities[n].Use(TARGET);
        takeAction();
    }

    public void useAbility(int n, List<Transform> TARGET)
    {
        abilities[n].Use(TARGET);
        takeAction();
    }

    public bool isDead()
    {
        return currHP <= 0;
    } 
}
