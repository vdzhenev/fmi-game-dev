using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private const int maxHP = 3;
    [SerializeField]
    private int currHP = maxHP;
    [SerializeField]
    private int keys = 0;
    
    GameObject respawn;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        respawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    public void dealDamage(int amount)
    {        
        currHP -= amount;
        print("Player has taken" + amount + " damage.");
        if(currHP<=0)
        {
            print("Player has died.");
            currHP = maxHP;
            transform.position = respawn.transform.position;
        }
    }
}
