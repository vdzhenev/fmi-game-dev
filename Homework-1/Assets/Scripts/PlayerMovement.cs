using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2 (30,30);
    SpriteRenderer obj_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");
        Vector3 resp_pos = new Vector3 (respawn.transform.position.x, respawn.transform.position.y, 0);
        transform.position = resp_pos;
    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(speed.x*inputX, 0, 0);
        movement.x = speed.x*inputX;

        movement*=Time.deltaTime;
        
        //Flips sprite based on movement direction 
        obj_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (inputX > 0f)
        {
            obj_SpriteRenderer.flipX = false;
        }
        else if (inputX < 0f)
        {
            obj_SpriteRenderer.flipX = true;
        }
        
        //Press Space to Jump 
        if (isOnGround()&&Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,speed.y), ForceMode2D.Impulse);
        }

        transform.Translate(movement);
        
    }

    private void FixedUpdate() 
    {
        
    }

    bool isOnGround()
    {
        int mask = LayerMask.GetMask("Player", "Ground");
        float rayLength = obj_SpriteRenderer.bounds.size.y/2 + 0.1f;
        Vector3 origin = transform.position;
        Vector3 direction = Vector2.down;
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, direction, rayLength, mask);
        
        return hitInfo.collider != null;
    }
}
