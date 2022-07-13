using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    private void Awake() 
    {
        //If the Map Canvas exists (meaning the player has already started a run)
        //The menu contains a continue button, which allows the player to continue their run
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
