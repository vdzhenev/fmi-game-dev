using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/SlowBuff")]
public class SlowBuff : Buff
{
    private CharacterStat cs;
    private int val;

    public SlowBuff(int dur, Transform obj, int v) : base(dur, obj, false)
    {
        cs = obj.GetComponent<CharacterStat>();
        val = v*10;
    }

    public void Init(int dur, Transform obj, int v) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        base.setStackable(false);
        base.setType(BuffBar.BuffType.Slow);
        base.BuffDescription = "This chraracter's accuracy is lowered, making them more likely to miss their next attack.";
        cs = obj.GetComponent<CharacterStat>();
        val = v*10;
    }

    protected override void ApplyEffect()
    {
        cs.ACC.setValue(cs.ACC.GetValue() - val);
    }

    public override void End()
    {
        cs.ACC.setValue(cs.ACC.GetValue() + val);
    }
}
