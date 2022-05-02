using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    private GameObject select;
    private SpriteRenderer sr;

    private void OnMouseOver() 
    {
        select = GameObject.FindGameObjectWithTag("Select");
        sr = select.GetComponent<SpriteRenderer>();
        select.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        sr.enabled = true;
    }

    private void OnMouseExit() 
    {
        sr = select.GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }
}
