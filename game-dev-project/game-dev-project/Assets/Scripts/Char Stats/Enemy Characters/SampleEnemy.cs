using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        abilities[0].setAction(Attack);
        abilities[0].setValue(10+STR.GetValue());
    }

    public void Attack(Transform target, int val)
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
        }
    }
}
