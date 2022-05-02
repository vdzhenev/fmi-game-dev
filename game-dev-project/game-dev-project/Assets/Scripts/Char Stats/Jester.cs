using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jester : CharacterStat
{
    protected override void Awake() 
    {
        base.Awake();
        ab1 = new Ability("Firebolt", Ability.Target.SingleEnemy, 10+(WIS.GetValue()*2), "Deals damage to a single enemy");
        ab2 = new Ability("Wall of Fire", Ability.Target.EnemyFrontCol, 5+(WIS.GetValue()/2), "Deals damage to all enemies in the front column");
        ab3 = new Ability("Slow", Ability.Target.SingleEnemy, WIS.GetValue(), "Slows down an enemy making them more likely to miss.");
        ab4 = new Ability("Haste", Ability.Target.SingleAlly, 1, "Ally can take two actions on it's next turn.");
    }

    private void Start() {
        
    }
}
