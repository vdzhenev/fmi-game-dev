using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject bottom = GameObject.FindGameObjectWithTag("Bottom");
        float bot_pos = bottom.transform.position.y;

        GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");
        Vector3 resp_pos = new Vector3 (respawn.transform.position.x, respawn.transform.position.y, 0);

        if(transform.position.y<bot_pos)
        {
            transform.position = resp_pos;
        }
    }
}
