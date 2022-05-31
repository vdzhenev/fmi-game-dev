using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyAimBuff : Buff
{
    private CharacterStat cs;

    public SteadyAimBuff(int dur, Transform obj) : base(dur, obj)
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
        cs.CRT.setValue(cs.CRT.GetValue() + 100);
    }

    public override void End()
    {
        cs.CRT.setValue(cs.CRT.GetValue() - 100);
    }
}
