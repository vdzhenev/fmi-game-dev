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
    public bool isFinished = false;

    [SerializeField] private Sprite image;

    public Buff(int dur, Transform obj)
    {
        Duration = dur;
        Obj = obj;
    }

    public void setDuration(int dur)
    {
        Duration = dur;
    }

    public void setObj(Transform obj) 
    {
        Obj = obj;
    }

    //Tick buff - effects get applied and duration is reduced
    public void Tick()
    {
        --Duration;
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
