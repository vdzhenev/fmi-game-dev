using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fjord : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        abilities.Add(new Ability("Falchion",           10+(STR.GetValue()),            -1,     false,  "Deals damage to an enemy.",                                                    Falchion));
        abilities.Add(new Ability("Hex",                1,                               2,     false,  "Next attack against an enemy will deal more damage. Enemy is weaker.",         Hex));
        abilities.Add(new Ability("Armor of Agathys",   WIS.GetValue(),                 -1,     false,   "Next attack against you will deal less damage. Attacker will take damage.",   ArmorOfAgathys));
        abilities.Add(new Ability("Hunger of Hadar",    WIS.GetValue()*2,                  1,    true,  "Deals damage to enemies in the back row.",                                     HungerOfHadar));
    }

    private void Falchion(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }

    private void Hex(Transform target, int val)
    {
        
    }

    private void ArmorOfAgathys(Transform target, int val)
    {
        
    }

    private void HungerOfHadar(Transform target, int val)
    {
        
    }
}
