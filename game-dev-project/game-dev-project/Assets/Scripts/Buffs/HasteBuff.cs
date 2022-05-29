using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteBuff : Buff
{
    private CharacterStat cs;

    public HasteBuff(int dur, Transform obj) : base(dur, obj)
    {
        cs = obj.GetComponent<CharacterStat>();
    }

    public void Init(int dur, Transform obj) 
    {
        base.setDuration(dur);
        base.setObj(obj);
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
