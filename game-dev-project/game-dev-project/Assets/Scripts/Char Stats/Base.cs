using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int BaseValue;

    public int GetValue()
    {
        return BaseValue;
    }
}
