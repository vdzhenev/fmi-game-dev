using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Ability a;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(a != null)
            TooltipScreenspace.ShowTooltip_Static(a.getAbilityText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipScreenspace.HideTooltip_Static();
    }
}
