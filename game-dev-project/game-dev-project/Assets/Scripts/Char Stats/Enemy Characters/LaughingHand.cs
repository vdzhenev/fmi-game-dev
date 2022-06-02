using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingHand : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        abilities[0].setAction(SwordArm);
        abilities[0].setValue(10+2*STR.GetValue());
        abilities[0].setTarget(Ability.Target.SingleEnemy);

        abilities[1].setAction(SummonHounds);
        abilities[1].setValue(1);
        abilities[1].setTarget(Ability.Target.Self);
        abilities[1].setUses(1);

        abilities[2].setAction(Frighten);
        abilities[2].setValue(1);
        abilities[2].setTarget(Ability.Target.EnemyFront);
        abilities[2].setUses(1);

    }

    public void SwordArm(Transform target, int val)
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
        }
        tickOnAttackBuffs();
    }

    public void SummonHounds(Transform target, int val)
    {
        //TODO Invisibility buff - can't be targeted directly by enemies
    }

    public void Frighten(Transform target, int val) 
    {
        //TODO Firghtened debuff - target loses an action
    }
}
