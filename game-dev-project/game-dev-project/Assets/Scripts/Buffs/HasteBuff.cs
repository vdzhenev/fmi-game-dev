using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/HasteBuff")]
public class HasteBuff : Buff
{
    private CharacterStat cs;

    public HasteBuff(int dur, Transform obj) : base(dur, obj, false)
    {
        cs = obj.GetComponent<CharacterStat>();
    }

    public void Init(int dur, Transform obj) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        base.setStackable(false);
        base.setType(BuffBar.BuffType.Haste);
        base.BuffDescription = "This character can take an extra action on their turn.";
        cs = obj.GetComponent<CharacterStat>();
    }

    protected override void ApplyEffect()
    {
        cs.baseNumOfActions += 1;
        cs.refreshActions();
    }

    public override void End()
    {
        cs.baseNumOfActions -=1;
    }
}
