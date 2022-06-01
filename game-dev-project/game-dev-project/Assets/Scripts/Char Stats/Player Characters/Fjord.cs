using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fjord : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();

        int str = STR.GetValue();
        int wis = WIS.GetValue();

        abilities[0].setAction(Falchion);
        abilities[0].setValue(10+(str));
        abilities[0].setTarget(Ability.Target.SingleEnemy);
        abilities[0].setDescription($"Deals <color=#B4323D>{10+str}</color> damage to a single enemy");

        abilities[1].setAction(Hex);
        abilities[1].setValue(wis/2);
        abilities[1].setTarget(Ability.Target.SingleEnemy);
        abilities[1].setDescription($"Next attack against the target will deal more damage.\nTarget is weaker for 1 turn.");

        abilities[2].setAction(ArmorOfAgathys);
        abilities[2].setValue(wis);
        abilities[2].setTarget(Ability.Target.Self);
        abilities[2].setDescription($"Next attack against you will deal less damage.\nAttacker will take <color=#6DA9DF>{wis}</color> damage.");

        abilities[3].setAction(HungerOfHadar);
        abilities[3].setValue(wis*2);
        abilities[3].setTarget(Ability.Target.EnemyBack);
        abilities[3].setDescription($"Deals <color=#6DA9DF>{wis*2}</color> damage to all enemies in the back row.");

        //abilities.Add(new Ability("Falchion",           10+(STR.GetValue()),            -1,     false,  "Deals damage to an enemy.",                                                    Falchion));
        //abilities.Add(new Ability("Hex",                1,                               2,     false,  "Next attack against an enemy will deal more damage. Enemy is weaker.",         Hex));
        //abilities.Add(new Ability("Armor of Agathys",   WIS.GetValue(),                 -1,     false,   "Next attack against you will deal less damage. Attacker will take damage.",   ArmorOfAgathys));
        //abilities.Add(new Ability("Hunger of Hadar",    WIS.GetValue()*2,                  1,    true,  "Deals damage to enemies in the back row.",                                     HungerOfHadar));
    }

    private void Falchion(Transform target, int val)
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

    private void Hex(Transform target, int val)
    {
        HexBuff hex = ScriptableObject.CreateInstance<HexBuff>();
        hex.Init(1, target, val);
        target.GetComponent<CharacterStat>().addBuff(hex);
    }

    private void ArmorOfAgathys(Transform target, int val)
    {
        AoABuff armor = ScriptableObject.CreateInstance<AoABuff>();
        armor.Init(1, target, val);
        target.GetComponent<CharacterStat>().addBuff(armor);
    }

    private void HungerOfHadar(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
        //DamagePopup.Create(target.position, val, false);
        SoundManager.PlaySound(SoundManager.Sound.Hit);
    }
}
