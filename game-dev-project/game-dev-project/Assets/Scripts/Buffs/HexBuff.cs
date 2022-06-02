using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/HexBuff")]
public class HexBuff : Buff
{
    private CharacterStat cs;
    private int val;

    public HexBuff(int dur, Transform obj, int v) : base(dur, obj, false)
    {
        cs = obj.GetComponent<CharacterStat>();
        val = v;
    }

    public void Init(int dur, Transform obj, int v) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        base.setStackable(false);
        base.setType(BuffBar.BuffType.Hex);
        base.BuffDescription = "This character's AC is lowered, making the next attack they suffer deal more damage.";
        cs = obj.GetComponent<CharacterStat>();
        val = v;
    }

    protected override void ApplyEffect()
    {
        cs.AC.setValue(cs.AC.GetValue() - val);
    }

    public override void End()
    {
        cs.AC.setValue(cs.AC.GetValue() + val);
    }
}
