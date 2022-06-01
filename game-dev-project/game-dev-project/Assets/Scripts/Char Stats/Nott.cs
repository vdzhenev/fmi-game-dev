using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nott : CharacterStat
{
        protected override void Awake() 
    {
        base.Awake();

        int dex = DEX.GetValue();
        
        abilities[0].setAction(Crossbow);
        abilities[0].setValue(10+dex*2);
        abilities[0].setTarget(Ability.Target.SingleEnemy);
        abilities[0].setDescription($"Deals <color=#0C9C19>{10+dex*2}</color> damage to a single enemy.");

        abilities[1].setAction(SteadyAim);
        abilities[1].setValue(0);
        abilities[1].setTarget(Ability.Target.Self);
        abilities[1].setDescription($"Your next attack will deal double damage!");

        abilities[2].setAction(Hide);
        abilities[2].setValue(0);
        abilities[2].setTarget(Ability.Target.Self);
        abilities[2].setDescription($"Enemies can\'t target you until your next turn.");

        abilities[3].setAction(Laughter);
        abilities[3].setValue(1);
        abilities[3].setTarget(Ability.Target.SingleEnemy);
        abilities[3].setDescription($"Enemy will have one less action next turn.");

        //abilities.Add(new Ability("Crossbow",           10+(DEX.GetValue()*2),  -1,     true,  "Deals damage to a single enemy",                    Crossbow));
        //abilities.Add(new Ability("Steady Aim",         0,                       2,     false,  "Next attack will deal double damage!",             SteadyAim));
        //abilities.Add(new Ability("Hide",               0,                      -1,     false,  "Enemies can\'t target you until your next turn",   Hide));
        //abilities.Add(new Ability("Hideous Laughter",   1,                       2,     false,  "Enemy will have one less action next turn.",       Laughter));
    }

    private void Crossbow(Transform target, int val)
    {
        bool crt = false;
        CharacterStat CS = target.GetComponent<CharacterStat>();
        if(Random.Range(1,100)<=ACC.GetValue())
        {
            if(Random.Range(1,100)<=CRT.GetValue())
            {
                val*=2;
                crt = true;
            }
            CS.takeDamage(val);
            DamagePopup.Create(target.position, val, crt);
        }
        else
        {
            DamagePopup.Create(target.position, $"<color=#42BFB7>MISS!</color>");
            SoundManager.PlaySound(SoundManager.Sound.Miss);
        }
    }

    private void SteadyAim(Transform target, int val)
    {
        SteadyAimBuff aim = ScriptableObject.CreateInstance<SteadyAimBuff>();
        aim.Init(1, target);
        aim.Activate();
        //target.GetComponent<CharacterStat>().addBuff(aim);
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
