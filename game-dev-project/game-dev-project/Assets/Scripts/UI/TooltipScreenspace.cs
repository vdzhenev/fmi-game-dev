using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipScreenspace : MonoBehaviour
{
    public static TooltipScreenspace Instance {get; private set;}

    [SerializeField] private RectTransform canvasRectTransfrom;

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;

    private System.Func<string> getTooltipTextFunc;

    private void Awake() 
    {
        Instance = this;
        
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();

        HideTooltip();
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(textMeshPro.margin.y*2, textMeshPro.margin.z*2);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void Update() 
    {
        if(getTooltipTextFunc != null)
            SetText(getTooltipTextFunc());

        //Moves rect depending on mouse position, pushes it down and left when at the top or at the right side of the screen
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransfrom.localScale.x;
        if(anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransfrom.rect.width )
        {
            anchoredPosition.x = canvasRectTransfrom.rect.width - backgroundRectTransform.rect.width;
        }
        if(anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransfrom.rect.height )
        {
            anchoredPosition.y = canvasRectTransfrom.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(string tooltipText)
    {
        this.getTooltipTextFunc = null;
        gameObject.SetActive(true);
        SetText(tooltipText);
 
    }

    private void ShowTooltip(System.Func<string> getTooltipTextFunc)
    {
        this.getTooltipTextFunc = getTooltipTextFunc;
        gameObject.SetActive(true);
        SetText(getTooltipTextFunc());
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipText)
    {
        Instance.ShowTooltip(tooltipText);
    }

    public static void ShowTooltip_Static(System.Func<string> getTooltipTextFunc)
    {
        Instance.ShowTooltip(getTooltipTextFunc);
    }

    public static void HideTooltip_Static()
    {
        Instance.HideTooltip();
    }
}
