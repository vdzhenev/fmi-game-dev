using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/SteadyAimBuff")]
public class SteadyAimBuff : Buff
{
    private CharacterStat cs;

    public SteadyAimBuff(int dur, Transform obj) : base(dur, obj, false)
    {
        cs = obj.GetComponent<CharacterStat>();
    }

    public SteadyAimBuff(Buff b) : base(b)
    {
        
    }

    public void Init(int dur, Transform obj) 
    {
        base.setDuration(dur);
        base.setObj(obj);
        base.setStackable(false);
        base.setType(BuffBar.BuffType.SteadyAim);
        this.BuffDescription = "This character has their crit chance increased, making them more likely to strike a critical hit on their next attack.";
        cs = obj.GetComponent<CharacterStat>();
    }

    protected override void ApplyEffect()
    {
        cs.CRT.setValue(cs.CRT.GetValue() + 100);
    }

    public override void End()
    {
        cs.CRT.setValue(cs.CRT.GetValue() - 100);
        Debug.Log("Ended steady aim, current crt " + cs.CRT.GetValue());
    }
}
