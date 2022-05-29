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
        textMeshPro.SetText($"<uppercase>{Character.name}</uppercase>\n"
                            + $"HP: {CS.currHP} / {CS.maxHP}\n"
                            + $"STR: <color=#B4323D>{CS.STR.GetValue()}</color>\n"
                            + $"DEX: <color=#0C9C19>{CS.DEX.GetValue()}</color>\n"
                            + $"WIS: <color=#6DA9DF>{CS.WIS.GetValue()}</color>\n"
                            + $"ACC: {(CS.acc > 100 ? 100 : CS.acc)}\n"
                            + $"CRT: {(CS.crit > 100 ? 100 : CS.crit)}");
    }
}
