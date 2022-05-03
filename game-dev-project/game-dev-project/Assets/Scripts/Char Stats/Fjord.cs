using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fjord : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();

        abilities[0].setAction(Falchion);
        abilities[0].setValue(10+(STR.GetValue()));

        abilities[1].setAction(Hex);
        abilities[1].setValue(1);

        abilities[2].setAction(ArmorOfAgathys);
        abilities[2].setValue(WIS.GetValue());

        abilities[3].setAction(HungerOfHadar);
        abilities[3].setValue(WIS.GetValue()*2);

        //abilities.Add(new Ability("Falchion",           10+(STR.GetValue()),            -1,     false,  "Deals damage to an enemy.",                                                    Falchion));
        //abilities.Add(new Ability("Hex",                1,                               2,     false,  "Next attack against an enemy will deal more damage. Enemy is weaker.",         Hex));
        //abilities.Add(new Ability("Armor of Agathys",   WIS.GetValue(),                 -1,     false,   "Next attack against you will deal less damage. Attacker will take damage.",   ArmorOfAgathys));
        //abilities.Add(new Ability("Hunger of Hadar",    WIS.GetValue()*2,                  1,    true,  "Deals damage to enemies in the back row.",                                     HungerOfHadar));
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
