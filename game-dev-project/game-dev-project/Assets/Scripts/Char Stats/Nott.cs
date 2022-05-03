using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nott : CharacterStat
{
        protected override void Awake() 
    {
        base.Awake();

        abilities[0].setAction(Crossbow);
        abilities[0].setValue(10+(DEX.GetValue()*2));

        abilities[1].setAction(SteadyAim);
        abilities[1].setValue(0);

        abilities[2].setAction(Hide);
        abilities[2].setValue(0);

        abilities[3].setAction(Laughter);
        abilities[3].setValue(1);

        //abilities.Add(new Ability("Crossbow",           10+(DEX.GetValue()*2),  -1,     true,  "Deals damage to a single enemy",                    Crossbow));
        //abilities.Add(new Ability("Steady Aim",         0,                       2,     false,  "Next attack will deal double damage!",             SteadyAim));
        //abilities.Add(new Ability("Hide",               0,                      -1,     false,  "Enemies can\'t target you until your next turn",   Hide));
        //abilities.Add(new Ability("Hideous Laughter",   1,                       2,     false,  "Enemy will have one less action next turn.",       Laughter));
    }

    private void Crossbow(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }

    private void SteadyAim(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeDamage(val);
    }

    private void Hide(Transform target, int val)
    {

    }

    private void Laughter(Transform target, int val)
    {
        CharacterStat CS = target.GetComponent<CharacterStat>();
        CS.takeAction();
    }
}
