using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleOver : MonoBehaviour
{
    [SerializeField] Transform buttonCont;
    [SerializeField] Transform buttonMenu;
    GameManager gameManager;
    Canvas canvas;
    
    private void Awake() 
    {
        canvas = GetComponent<Canvas>();
        gameManager = FindObjectOfType<GameManager>();
    }

    //Shows the canvas
    //Depending on the outcome of the battle, the canvas shows or hides the continue button
    public void Show(bool won)
    {
        canvas.enabled = true;
        if(!won)
        {
            buttonCont.gameObject.SetActive(false);
        }
        else
        {
            buttonCont.gameObject.SetActive(true);
        }
    }

    //Hides the canvas
    public void Hide()
    {
        canvas.enabled = false; 
    }
}
