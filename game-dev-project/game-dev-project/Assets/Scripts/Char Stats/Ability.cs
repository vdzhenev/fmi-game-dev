using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability")]
public class Ability : ScriptableObject
{

    [SerializeField] private Sprite image;

    [SerializeField]
    private string abilityName = "sample name";

    [SerializeField]
    private string description = "sample description";

    [SerializeField] private int Value = 0;
    [SerializeField] private int BaseUses = -1;
    private int Uses {set; get;}
    private bool special = false;

    private Action<Transform, int> onUse;

    public void setAction(Action<Transform, int> action)
    {
        onUse = action;
    }

    public void setValue(int val) 
    {
        Value = val;
    }

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

    public Sprite getImage()
    {
        return image;
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

    public string getAbilityText()
    {
        return(abilityName + "\n" + description);
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
