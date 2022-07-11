using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffBar : MonoBehaviour
{
    Dictionary<BuffType, GameObject> _dictionary;

    public enum BuffType
    {
        AoA,
        Haste,
        Hex,
        Hide,
        Slow,
        SteadyAim,
        Weaken
    }

    private void Awake() 
    {
        _dictionary = new Dictionary<BuffType, GameObject>();
    }


    public void AddBuff(Buff buff)
    {
        Debug.Log("Type is " + buff.type);
        GameObject newBuff = new GameObject();
        newBuff.transform.parent = transform;
        Image buffSprite = newBuff.AddComponent<Image>();
        buffSprite.sprite = getBuffIcon(buff.type);

        RectTransform nbRT = newBuff.GetComponent<RectTransform>();
        RectTransform parentRT = transform.GetComponent<RectTransform>();
        nbRT.sizeDelta = parentRT.rect.height * Vector3.one;

        BuffTooltipTrigger tooltipTrigger = newBuff.AddComponent<BuffTooltipTrigger>();
        tooltipTrigger.text = buff.BuffDescription;

        _dictionary.Add(buff.type, newBuff);
    }

    public void RemoveBuff(BuffType type)
    {
        GameObject toRemove = _dictionary[type];
        _dictionary.Remove(type);
        Destroy(toRemove);
    }

    private static Sprite getBuffIcon(BuffType type)
    {
        foreach(GameAssets.BuffIcon buffIcon in GameAssets.i.buffIconArray)
        {
            if(buffIcon.type == type)
            {
                return buffIcon.icon;
            }
        }
        Debug.Log("Buff type " + type + " not found!");
        return null;
    }
}
