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

    public void Attack(Transform Target, int val)
    {
        CharacterStat CS = Target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }
}
