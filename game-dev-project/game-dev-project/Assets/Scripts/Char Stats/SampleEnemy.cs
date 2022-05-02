using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        ab1 = new Ability("Attack", Ability.Target.SingleAlly, 10 + STR.GetValue(), "Simple attack");
    }

    public void Attack(Transform Target)
    {
        Target.GetComponent<CharacterStat>().takeDamage(ab1.getVal());
    }
}
