using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDest : MonoBehaviour
{
    public Vector3 to;
    public float speed;

    private void Awake() 
    {
        speed = 6f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, to, step);

        if (Vector3.Distance(transform.position, to) < 0.001f)
        {
            Destroy(gameObject);
        }
    }
}
