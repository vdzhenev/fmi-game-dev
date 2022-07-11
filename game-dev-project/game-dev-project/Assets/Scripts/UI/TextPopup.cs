using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    TextMeshPro text;
    private float timer = 2f;
    private bool show;
    // Start is called before the first frame update
    void Awake()
    {
        show = false;
        text = transform.Find("Text").GetComponent<TextMeshPro>();
        gameObject.SetActive(false);
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
        //if(!gameObject.activeSelf)
        {
            
            gameObject.SetActive(true);
            Debug.Log(gameObject.activeSelf);
            text.SetText(txt);
            StartCoroutine(HideAfter2Seconds());
        }
    }

    //private void Update() 
    //{
    //    timer -= Time.deltaTime;
    //    if(!show || timer <= 0)
    //    {
    //        //Destroy(gameObject);
    //    }
    //}

    IEnumerator HideAfter2Seconds()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
