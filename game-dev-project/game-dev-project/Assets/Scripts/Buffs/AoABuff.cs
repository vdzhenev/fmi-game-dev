using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/AoABuff")]
public class AoABuff : Buff
{
    private CharacterStat cs;
    private int val;

    public AoABuff(int dur, Transform obj, int v) : base(dur, obj, false)
    {
        cs = obj.GetComponent<CharacterStat>();
        val = v;
    }

    public void Init(int dur, Transform obj, int v) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        base.setStackable(false);
        base.setType(BuffBar.BuffType.AoA);
        base.BuffDescription = "Next attack against this character will deal less damage.";
        cs = obj.GetComponent<CharacterStat>();
        val = v;
    }

    protected override void ApplyEffect()
    {
        cs.AC.setValue(cs.AC.GetValue() + val/2);
    }

    public override void End()
    {
        cs.AC.setValue(cs.AC.GetValue() - val/2);
    }
}
