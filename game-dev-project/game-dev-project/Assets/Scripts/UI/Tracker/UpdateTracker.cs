using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTracker : MonoBehaviour
{

    private int turnCount;
    [SerializeField]
    private GameObject[] Turns;
    private List<int> initiativeIDs;
    private int iniLen;

    private void Awake() 
    {
        turnCount = -1;
        initiativeIDs = new List<int>();
    }

    public void Setup(List<Transform> ini, int len)
    {   
        iniLen = len;
        for (int i = 0; i<iniLen; ++i)
        {
            int currID = ini[i].GetComponent<CharacterStat>().ID;
            initiativeIDs.Add(currID);
        }
    }

    public void UpdateUI()
    {
        ++turnCount;
        for(int i = 0; i<5; ++i)
        {
            Turns[i].GetComponent<Image>().sprite = Turns[i].GetComponent<SpriteHandler>().Icons[initiativeIDs[(i+turnCount)%iniLen]];
        }

    }
}
