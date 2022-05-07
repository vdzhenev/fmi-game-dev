using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HpBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Slider slider;
    public TextMeshProUGUI text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.gameObject.SetActive(false);
    }

    public void SetMaxHealth(int val)
    {
        slider.maxValue = val;
        slider.value = val;
        text.SetText($"{val} / {val}");
    }

    public void SetHealth(int val)
    {
        slider.value = val;
        text.SetText($"{val} / {slider.maxValue}");
    }
}
