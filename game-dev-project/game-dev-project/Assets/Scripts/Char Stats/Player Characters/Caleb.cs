using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caleb : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();

        int wis = WIS.GetValue();
        abilities[0].setAction(Firebolt);
        abilities[0].setValue(10+(wis*2));
        abilities[0].setTarget(Ability.Target.SingleEnemy);
        abilities[0].setDescription($"Deals <color=#6DA9DF>{10+wis*2}</color> damage to a single enemy");
        abilities[0].setStart(true);

        abilities[1].setAction(WallOfFire);
        abilities[1].setValue(5+(wis/2));
        abilities[1].setTarget(Ability.Target.EnemyFront);
        abilities[1].setDescription($"Deals <color=#6DA9DF>{5+wis/2}</color> damage to all enemies in the front column");

        abilities[2].setAction(Slow);
        abilities[2].setValue(wis);
        abilities[2].setTarget(Ability.Target.SingleEnemy);
        abilities[2].setDescription($"Slows down an enemy making them more likely to miss their next attack.");

        abilities[3].setAction(Haste);
        abilities[3].setValue(1);
        abilities[3].setTarget(Ability.Target.SingleAlly);
        abilities[3].setDescription($"Ally can take an extra action on it\'s next turn");

        //              NAME                                VALUE           USES    SPECIAL             DESCRIPTION                                     ACTION
        //abilities.Add(new Ability("Firebolt",       10+(WIS.GetValue()*2),  -1,     false,  "Deals damage to a single enemy",                       Firebolt));
        //abilities.Add(new Ability("Wall of Fire",   5+(WIS.GetValue()/2),   2,      true,   "Deals damage to all enemies in the front column",      WallOfFire));
        //abilities.Add(new Ability("Slow",           WIS.GetValue(),         -1,     false,  "Slows down an enemy making them more likely to miss.", Slow));
        //abilities.Add(new Ability("Haste",          1,                      -1,     false,  "Ally can take two actions on it's next turn.",         Haste));
    }

    private void Firebolt(Transform target, int val)
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
            CS.takeDamage(val, crt);
            //DamagePopup.Create(target.position, val, crt);
        }
        else
        {
            DamagePopup.Create(target.position, $"<color=#42BFB7>MISS!</color>");
            SoundManager.PlaySound(SoundManager.Sound.Miss);
        }
        tickOnAttackBuffs();
    }

    private void WallOfFire(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val, false);
        tickOnAttackBuffs();
        DamagePopup.Create(target.position, val, false);
        SoundManager.PlaySound(SoundManager.Sound.Hit);
    }

    private void Slow(Transform target, int val)
    {
        SlowBuff slow = ScriptableObject.CreateInstance<SlowBuff>();
        slow.Init(1, target, val);
        target.GetComponent<CharacterStat>().addTimedBuff(slow);
    }

    private void Haste(Transform target, int val)
    {
        HasteBuff haste = ScriptableObject.CreateInstance<HasteBuff>();
        haste.Init(val, target);
        target.GetComponent<CharacterStat>().addTimedBuff(haste);
    }
}
