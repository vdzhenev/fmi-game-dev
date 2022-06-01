using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    TextMeshPro text;
    private float timer = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        text = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public static TextPopup Create(Vector3 position, string text)
    {
        position += new Vector3(0, 1, 0);
        Transform textPopupTransform = Instantiate(GameAssets.i.pfTextPopup, position, Quaternion.identity);
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();

        textPopup.Setup(text);
        return textPopup; 
    }

    public void Setup(string txt) 
    {
        text.SetText(txt);
    }

    private void Update() 
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
