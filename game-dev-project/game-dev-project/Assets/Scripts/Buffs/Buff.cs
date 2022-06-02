using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff")]
public abstract class Buff : ScriptableObject
{
    //Buff has duration, object which gets affected, and a boolen which tells when the duration is over
    public int Duration;
    private Transform Obj;
    private bool isStackable;
    public bool isFinished = false;
    public BuffBar.BuffType type;
    public string BuffDescription;

    [SerializeField] private Sprite Icon;

    public Buff(int dur, Transform obj, bool stack)
    {
        Duration = dur;
        Obj = obj;
        isStackable = stack;
    }

    public Buff(Buff b) 
    {
        this.Duration = b.Duration;
        this.Obj = b.Obj;
        this.isStackable = b.isStackable;
        this.isFinished = b.isFinished;
        this.type = b.type;
        this.BuffDescription = b.BuffDescription;
    }

    public void setDuration(int dur)
    {
        Duration = dur;
    }

    public void setObj(Transform obj) 
    {
        Obj = obj;
    }

    public void setStackable(bool stack)
    {
        isStackable = stack;
    }

    public void setType(BuffBar.BuffType _type)
    {
        type = _type;
    }

    public Sprite getIcon()
    {
        return Icon;
    }

    public string getDescription()
    {
        return BuffDescription;
    }

    //Tick buff - effects get applied and duration is reduced
    public void Tick()
    {
        --Duration;
        if(isStackable)
            ApplyEffect();
        if(Duration <= 0)
        {
            End();
            isFinished = true;
        }
    }

    public void Activate()
    {
        ApplyEffect();
    }

    protected abstract void ApplyEffect();

    public abstract void End();
}
