using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        abilities.Add(new Ability("Attack", 10 + STR.GetValue(), -1, false, "Simple attack", Attack));
    }

    public void Attack(Transform Target, int val)
    {
        CharacterStat CS = Target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }
}
