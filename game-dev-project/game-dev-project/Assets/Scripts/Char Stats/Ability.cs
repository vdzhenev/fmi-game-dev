using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability 
{
    private int Value = 0;
    private int BaseUses = -1;
    private int Uses {set; get;}
    private bool special = false;
    private Action<Transform, int> onUse;

    [SerializeField]
    private string name = "sample name";
    private string description = "sample description";

    private void Awake() 
    {
        Uses = BaseUses;
    }

    public string getDescription()
    {
        return description;
    }

    public int getVal()
    {
        return Value;
    }

    public void refreshUses()
    {
        Uses = BaseUses;
    }

    public Ability(string n, int val, int u, bool s, string desc, Action<Transform, int> _onUse)
    {
        name = string.Copy(n);
        Value = val;
        Uses = u;
        special = s;
        description = string.Copy(desc);
        onUse = _onUse;
    }

    public void readAb()
    {
        Debug.Log(name + "\nValue " + Value + "\n Descr " + description);
    }

    public void Use(Transform TARGET)
    {
        if(Uses==0)
        {
            Debug.Log("No more uses left!");
            return;
        }
        else if(Uses != -1)
        {
            --Uses;
        }
        onUse(TARGET, Value);
    }

}
