using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    private void Awake() 
    {
        if(GameObject.Find("MapCanvas") != null)
            GameObject.Find("MenuCanvas").GetComponent<MenuCanvas>().showContButton(true);
        else
            GameObject.Find("MenuCanvas").GetComponent<MenuCanvas>().showContButton(false);
    }

    public void showContButton(bool show)
    {
        transform.GetChild(0).GetComponent<MenuButtons>().ShowHideContButton(show);
    }
}
