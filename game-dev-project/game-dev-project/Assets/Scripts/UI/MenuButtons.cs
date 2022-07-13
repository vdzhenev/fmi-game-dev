using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject ContButton;

    public void ShowHideContButton(bool show)
    {
        ContButton.SetActive(show);
    }
}
