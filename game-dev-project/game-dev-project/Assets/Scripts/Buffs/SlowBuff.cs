using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBuff : Buff
{
    private CharacterStat cs;
    private int val;

    public SlowBuff(int dur, Transform obj, int v) : base(dur, obj)
    {
        cs = obj.GetComponent<CharacterStat>();
        val = v*10;
    }

    public void Init(int dur, Transform obj, int v) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        cs = obj.GetComponent<CharacterStat>();
        val = v;
    }

    protected override void ApplyEffect()
    {
        cs.acc -= val;
    }

    public override void End()
    {
        cs.acc += val;
    }
}
