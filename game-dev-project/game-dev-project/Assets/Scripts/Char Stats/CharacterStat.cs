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
    public List<Ability> abilities;

    
    public Transform myTarget {get; set;}

    protected virtual void Awake() 
    {
        abilities = new List<Ability>();
        currHP = maxHP;
        refreshActions();
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
    }

    public void heal(int amount)
    {
        if(!isDead())
        {
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
    }

    public bool isDead()
    {
        return currHP <= 0;
    } 
}
