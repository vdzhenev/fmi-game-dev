using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability 
{
    public enum Target
    {
        Self,
        SingleEnemy,
        SingleAlly,
        EnemyFrontCol,
        EnemyBackCol
    }

    private Target target = Target.SingleEnemy;
    private int Value = 0;

    [SerializeField]
    private string name = "sample name";
    private string description = "sample description";

    public Target getTarget()
    {
        return target;
    }

    public string getDescription()
    {
        return description;
    }

    public int getVal()
    {
        return Value;
    }

    public Ability(string n, Target t, int val, string desc)
    {
        target = t;
        Value = val;
        name = string.Copy(n);
        description = string.Copy(desc);
    }

    public void readAb()
    {
        Debug.Log(name + "\nTarget " + target + "\nValue " + Value + "\n Descr " + description);
    }

}
