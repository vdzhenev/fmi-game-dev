using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caleb : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        abilities.Add(new Ability("Firebolt",       10+(WIS.GetValue()*2),  -1,     false,  "Deals damage to a single enemy",                       Firebolt));
        abilities.Add(new Ability("Wall of Fire",   5+(WIS.GetValue()/2),   2,      true,   "Deals damage to all enemies in the front column",      WallOfFire));
        abilities.Add(new Ability("Slow",           WIS.GetValue(),         -1,     false,  "Slows down an enemy making them more likely to miss.", Slow));
        abilities.Add(new Ability("Haste",          1,                      -1,     false,  "Ally can take two actions on it's next turn.",         Haste));
    }

    private void Firebolt(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }

    private void WallOfFire(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }

    private void Slow(Transform target, int val)
    {

    }

    private void Haste(Transform target, int val)
    {

    }
}
