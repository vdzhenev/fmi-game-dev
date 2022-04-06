using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    GameObject respawn;
    // Start is called before the first frame update
    void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.transform.position = respawn.transform.position;
            GetComponent<HurtPlayer>().dealOneDmg();
        }
    }
}
