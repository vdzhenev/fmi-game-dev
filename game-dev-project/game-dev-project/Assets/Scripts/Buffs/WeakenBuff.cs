using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/WeakenBuff")]
public class WeakenBuff : Buff
{
    private CharacterStat cs;
    private int val;

    public WeakenBuff(int dur, Transform obj, int v) : base(dur, obj, false)
    {
        cs = obj.GetComponent<CharacterStat>();
        val = v;
    }

    public void Init(int dur, Transform obj, int v) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        base.setStackable(false);
        base.setType(BuffBar.BuffType.Weaken);
        base.BuffDescription = "This character has their strength reduced, making their STR-based attacks deal less damage.";
        cs = obj.GetComponent<CharacterStat>();
        val = v;
    }

    protected override void ApplyEffect()
    {
        cs.STR.setValue(cs.STR.GetValue() - val);
    }

    public override void End()
    {
        cs.STR.setValue(cs.STR.GetValue() + val);
        Debug.Log("Ended weaken, curr STR " + cs.STR.GetValue());
    }
}
