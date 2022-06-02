using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int BaseValue;
    [SerializeField]
    private int CurrVal;

    public int GetValue()
    {
        return CurrVal;
    }

    public void setValue(int val) 
    {
        CurrVal = val;
    }

    public void setToBase()
    {
        CurrVal = BaseValue;
    }
}
