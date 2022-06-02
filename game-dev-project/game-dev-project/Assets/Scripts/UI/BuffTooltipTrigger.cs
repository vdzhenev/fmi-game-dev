using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipScreenspace.ShowTooltip_Static(text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipScreenspace.HideTooltip_Static();
    }
}
