using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();

        int str = STR.GetValue();
        int wis = WIS.GetValue();

        abilities[0].setAction(CureWounds);
        abilities[0].setValue(10+wis*2);
        abilities[0].setTarget(Ability.Target.SingleAlly);
        abilities[0].setDescription($"Heals an ally for <color=#6DA9DF>{10+wis*2}</color> HP.");

        abilities[1].setAction(PrayerOfHealing);
        abilities[1].setValue(5+(WIS.GetValue()/2));
        abilities[1].setTarget(Ability.Target.AllAllies);
        abilities[1].setDescription($"Heals all allies for <color=#6DA9DF>{5+wis/2}</color> HP.");

        abilities[2].setAction(SpiritualWeapon);
        abilities[2].setValue(10+str);
        abilities[2].setTarget(Ability.Target.SingleEnemy);
        abilities[2].setDescription($"Deals <color=#B4323D>{10+str}</color> damage to a single enemy.");

        abilities[3].setAction(SpiritGuardians);
        abilities[3].setValue(wis);
        abilities[3].setTarget(Ability.Target.Self);
        abilities[3].setDescription($"Enemies will take <color=#6DA9DF>{wis}</color> damage when attacking you.");

        //abilities.Add(new Ability("Cure Wounds",        10+(WIS.GetValue()),            -1,     false,  "Heals an ally.",                           CureWounds));
        //abilities.Add(new Ability("Prayer of Healing",  5+(WIS.GetValue()/2),            2,     false,  "Heals all allies.",                        PrayerOfHealing));
        //abilities.Add(new Ability("Spiritual Weapon",   10+(int)(WIS.GetValue()*1.8f),  -1,     true,   "Deals damage to an enemy.",                SpiritualWeapon));
        //abilities.Add(new Ability("Spirit Guardians",   WIS.GetValue(),                  1,     false,  "Enemies attacking you will take damage.",  SpiritGuardians));
    }

    private void CureWounds(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.heal(val);
        DamagePopup.Create(target.position, $"<color=#26FF00>{val}</color>");
    }

    private void PrayerOfHealing(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.heal(val);
        DamagePopup.Create(target.position, $"<color=#26FF00>{val}</color>");
    }

    private void SpiritualWeapon(Transform target, int val)
    {
        bool crt = false;
        CharacterStat CS = target.GetComponent<CharacterStat>();
        if(Random.Range(1,100)<=ACC.GetValue())
        {
            if(Random.Range(1,100)<=CRT.GetValue())
            {
                val*=2;
                crt = true;
            }
            CS.takeDamage(val);
            //DamagePopup.Create(target.position, val, crt);
        }
        else
        {
            DamagePopup.Create(target.position, $"<color=#42BFB7>MISS!</color>");
            SoundManager.PlaySound(SoundManager.Sound.Miss);
        }
    }

    private void SpiritGuardians(Transform target, int val)
    {

    }
}
