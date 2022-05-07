using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTracker : MonoBehaviour
{

    private int turnCount;
    [SerializeField]
    private GameObject[] Turns;
    private List<List<Sprite>> initiativeIcons;
    private int iniLen;

    private void Awake() 
    {
        turnCount = -1;
        initiativeIcons = new List<List<Sprite>>();
    }

    public void Setup(List<Transform> ini, int len)
    {   
        iniLen = len;
        for (int i = 0; i<iniLen; ++i)
        {
            initiativeIcons.Add(ini[i].GetComponent<CharacterStat>().Icons);
        }
    }

    public void UpdateUI()
    {
        ++turnCount;
        Turns[0].GetComponent<Image>().sprite = initiativeIcons[turnCount%iniLen][0];
        for(int i = 1; i<5; ++i)
        {
            Turns[i].GetComponent<Image>().sprite = initiativeIcons[(turnCount+i)%iniLen][1];
        }

    }
}
