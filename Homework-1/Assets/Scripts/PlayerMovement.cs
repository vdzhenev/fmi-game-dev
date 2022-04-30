using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2 (0,0);
    public float fallMult = 2.5f;
    public Animator animator;
    float fastFall;
    SpriteRenderer obj_SpriteRenderer;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        fastFall = Physics2D.gravity.y * (fallMult-1);
        rb = GetComponent<Rigidbody2D>();
        GameObject respawn = GameObject.FindGameObjectWithTag("Respawn");
        Vector3 resp_pos = new Vector3 (respawn.transform.position.x, respawn.transform.position.y, 0);
        transform.position = resp_pos;
    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(speed.x*inputX, 0, 0);
        animator.SetFloat("Horiz Speed", Mathf.Abs(movement.x));
        animator.SetFloat("Vert Speed", Mathf.Abs(rb.velocity.y));
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
        if (Input.GetKeyDown(KeyCode.Space)&&isOnGround())
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up*speed.y, ForceMode2D.Impulse);
        }

        if(rb.velocity.y<0 && rb.velocity.y >-10)
        {
            rb.velocity += Vector2.up * (fastFall * Time.deltaTime);
        }
        
        if (inputX != 0f)
        {
            rb.velocity = new Vector2(movement.x, rb.velocity.y);
        }
        
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
