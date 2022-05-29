using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBuff : Buff
{
    private CharacterStat cs;
    private int val;

    public HexBuff(int dur, Transform obj, int v) : base(dur, obj)
    {
        cs = obj.GetComponent<CharacterStat>();
        val = v;
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
        cs.AC.setValue(cs.AC.GetValue() - val);
        cs.STR.setValue(cs.AC.GetValue() - val);
    }

    public override void End()
    {
        cs.AC.setValue(cs.AC.GetValue() + val);
        cs.STR.setValue(cs.AC.GetValue() + val);
    }
}
