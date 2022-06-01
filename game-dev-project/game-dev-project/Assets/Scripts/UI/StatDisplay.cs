using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public static StatDisplay Instance {get; private set;}

    private Image charIcon;
    private TextMeshProUGUI textMeshPro;

    private void Awake() 
    {
        Instance = this;
        charIcon = transform.Find("icon").GetComponent<Image>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
    }

    public void updateStatDisplay(Transform Character)
    {
        CharacterStat CS = Character.GetComponent<CharacterStat>();
        charIcon.sprite = CS.Icons[0];
        textMeshPro.SetText($"<uppercase>{Character.name}</uppercase>\n"
                            + $"HP: {CS.currHP} / {CS.maxHP} \tAC: {CS.AC.GetValue()}\n"
                            + $"STR: <color=#B4323D>{CS.STR.GetValue()}</color>\n"
                            + $"DEX: <color=#0C9C19>{CS.DEX.GetValue()}</color>\n"
                            + $"WIS: <color=#6DA9DF>{CS.WIS.GetValue()}</color>\n"
                            + $"ACC: {(CS.ACC.GetValue() > 100 ? 100 : CS.ACC.GetValue())}\n"
                            + $"CRT: {(CS.CRT.GetValue() > 100 ? 100 : CS.CRT.GetValue())}");
    }
}
