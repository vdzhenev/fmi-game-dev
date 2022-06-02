using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/HideBuff")]
public class HideBuff : Buff
{
    private CharacterStat cs;

    public HideBuff(int dur, Transform obj) : base(dur, obj, false)
    {
        cs = obj.GetComponent<CharacterStat>();
    }

    public void Init(int dur, Transform obj) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        base.setStackable(false);
        base.setType(BuffBar.BuffType.Hide);
        base.BuffDescription = "This character is hidden, making them untargetable by enemy attacks.";
        cs = obj.GetComponent<CharacterStat>();
    }

    protected override void ApplyEffect()
    {
        cs.canBeTargeted = false;
    }

    public override void End()
    {
        cs.canBeTargeted = true;
    }
}
