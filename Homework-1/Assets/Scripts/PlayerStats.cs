using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private const int maxHP = 3;
    [SerializeField]
    private int currHP = maxHP;
    [SerializeField]
    private int keys = 0;
    private const int maxKeys = 3;
    
    public Image[] heartsHUD;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] keysHUD;
    public Sprite fullKey;
    public Sprite emptyKey;

    GameObject respawn;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        respawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    public void dealDamage(int amount)
    {        
        currHP -= amount;
        print("Player has taken" + amount + " damage.");
        heartsHUD[currHP].sprite=emptyHeart;
        if(currHP<=0)
        {
            print("Player has died.");
            //currHP = maxHP;
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }

    public void addKey()
    {
        if(keys<maxKeys)
        {
            keysHUD[keys].sprite = fullKey;
            print("Player received a key.");
            keys += 1;
        }
    }
}
