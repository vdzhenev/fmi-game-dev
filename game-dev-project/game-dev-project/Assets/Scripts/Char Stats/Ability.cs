using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ability")]
public class Ability : ScriptableObject
{
    public enum Target
    {
        SingleEnemy,
        Self,
        SingleAlly,
        AllAllies,
        EnemyFront,
        EnemyBack
    }

    [SerializeField] private Sprite image;

    [SerializeField]
    private string abilityName = "sample name";

    [SerializeField]
    private string description = "sample description";

    private Target target = Target.SingleEnemy;

    [SerializeField] private int Value = 0;
    [SerializeField] private int BaseUses = -1;
    private int Uses {set; get;}

    private bool special = false;

    private Action<Transform, int> onUse;

    private void Awake() 
    {
        Uses = BaseUses;
    }

    public void setAction(Action<Transform, int> action)
    {
        onUse = action;
    }

    //public void setAction(Action<List<Transform>, int> action) 
    //{
    //    onUse = action;
    //}

    public void setTarget(Target t) 
    {
        target = t;
    }

    public Target getTarget()
    {
        return target;
    }

    public void setValue(int val) 
    {
        Value = val;
    }

    public void setDescription(string d) 
    {
        description = string.Copy(d);
    }

    public string getDescription()
    {
        return description;
    }

    public int getVal()
    {
        return Value;
    }

    public int getUses()
    {
        return Uses;
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
        string toReturn = $"<color=#000000> {abilityName}\n{description}</color>";
        if(Uses!=-1)
        {
            toReturn+=$"<color=#000000>\nUses: {Uses}</color>";
        }
        return(toReturn);
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

    public void Use(List<Transform> Targets)
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
        foreach(Transform t in Targets)
        {
            onUse(t, Value);
        }
    }

}
