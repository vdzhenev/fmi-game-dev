using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    TextMeshPro text;
    private float timer;

    public static DamagePopup Create(Vector3 position, int dmgAmount, bool isCrit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();

        damagePopup.Setup(dmgAmount, isCrit);
        return damagePopup; 
    }

    public static DamagePopup Create(Vector3 position, string txt)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();

        damagePopup.Setup(txt);
        return damagePopup; 
    }

    private void Awake() 
    {
        text = transform.GetComponent<TextMeshPro>();
        timer = 1;
    }

    public void Setup(string txt) 
    {
        text.SetText(txt);
    }

    public void Setup(int dmgAmount, bool isCrit) 
    {
        
        if(isCrit)
        {
            text.SetText($"<color=#D25727>CRIT!\n{dmgAmount}</color>");
        }
        else
        {
            text.SetText(dmgAmount.ToString());
        }
    }

    private void Update() 
    {
        transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
