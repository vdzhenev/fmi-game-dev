using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rest : MonoBehaviour
{
    [SerializeField] private int healPercent;
    [SerializeField] private Transform[] PlayerPortraits;
    private GameObject[] players;

    private void Awake() 
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        int len = players.Length;
        for(int i = 0; i<len; ++i) 
        {
            CharacterStat curr = players[i].transform.GetComponent<CharacterStat>();
            PlayerPortraits[i].GetChild(0).GetComponent<HpBar>().SetMaxHealth(curr.maxHP);
            PlayerPortraits[i].GetChild(0).GetComponent<HpBar>().SetHealth(curr.currHP);
            PlayerPortraits[i].GetComponent<Image>().sprite = curr.Icons[0];
        }
    }

    public void RestPlayers()
    {
        int len = players.Length;
        for(int i = 0; i<len; ++i) 
        {
            CharacterStat curr = players[i].transform.GetComponent<CharacterStat>();
            float healAmount = healPercent/100f * curr.maxHP;
            curr.heal((int)healAmount);
            PlayerPortraits[i].GetChild(0).GetComponent<HpBar>().SetHealth(curr.currHP);
        }
        gameObject.GetComponent<Button>().interactable = false;
    }
}
