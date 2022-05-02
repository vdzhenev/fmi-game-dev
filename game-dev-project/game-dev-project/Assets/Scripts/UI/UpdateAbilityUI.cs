using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAbilityUI : MonoBehaviour
{
    [SerializeField]
    private Sprite[] p0Abilities;
    [SerializeField]
    private Sprite[] p1Abilities;
    [SerializeField]
    private Sprite[] p2Abilities;
    [SerializeField]
    private Sprite[] p3Abilities;

    [SerializeField]
    private Button[] buttons;

    private void Start() 
    {
        
    }

    public void updateAbilities(int ID)
    {
        switch (ID)
        {
            case 0:
                for(int i =0; i<4; ++i)
                {
                    buttons[i].GetComponent<Image>().sprite = p0Abilities[i];
                }
                break;
            case 1:
                for(int i =0; i<4; ++i)
                {
                    buttons[i].GetComponent<Image>().sprite = p1Abilities[i];
                }
                break;
            case 2:
                for(int i =0; i<4; ++i)
                {
                    buttons[i].GetComponent<Image>().sprite = p2Abilities[i];
                }
                break;
            case 3:
                for(int i =0; i<4; ++i)
                {
                    buttons[i].GetComponent<Image>().sprite = p3Abilities[i];
                }
                break;
            default:
                break;
        }
    }

}
