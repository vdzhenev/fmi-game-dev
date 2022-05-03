using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAbilityUI : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    private List<Ability> abilities;
    CharacterStat CS;

    public void updateAbilities(Transform currPlayer)
    {
        CharacterStat CS = currPlayer.GetComponent<CharacterStat>();
        for(int i = 0; i<4; ++i)
        {
            buttons[i].GetComponent<Image>().sprite = CS.abilities[i].getImage();
            buttons[i].GetComponent<TooltipTrigger>().a = CS.abilities[i];
            //abilities.Add(CS.abilities[i]);
        }   
    }

    public void useAbility(int n, Transform TARGET)
    {
        CS.useAbility(n, TARGET);
    }

}
