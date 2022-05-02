using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        abilities.Add(new Ability("Cure Wounds",        10+(WIS.GetValue()),            -1,     false,  "Heals an ally.",                           CureWounds));
        abilities.Add(new Ability("Prayer of Healing",  5+(WIS.GetValue()/2),            2,     false,  "Heals all allies.",                        PrayerOfHealing));
        abilities.Add(new Ability("Spiritual Weapon",   10+(int)(WIS.GetValue()*1.8f),  -1,     true,   "Deals damage to an enemy.",                SpiritualWeapon));
        abilities.Add(new Ability("Spirit Guardians",   WIS.GetValue(),                  1,     false,  "Enemies attacking you will take damage.",  SpiritGuardians));
    }

    private void CureWounds(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.heal(val);
    }

    private void PrayerOfHealing(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.heal(val);
    }

    private void SpiritualWeapon(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }

    private void SpiritGuardians(Transform target, int val)
    {

    }
}
