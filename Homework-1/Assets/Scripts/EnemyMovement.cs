using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int speed = 1;
    SpriteRenderer obj_SpriteRenderer;
    Rigidbody2D rb;
    Vector3 movement;
    
    public float Timer = 2f;
    public float ResetTimer = 2f;


    void Start()
    {
       obj_SpriteRenderer = GetComponent<SpriteRenderer>();
       rb = GetComponent<Rigidbody2D>();
       movement = new Vector3(speed, 0, 0);
       GameObject player = GameObject.FindGameObjectWithTag("Player");
       Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = movement;
        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            movement *= -1;
            Timer = ResetTimer;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<HurtPlayer>().dealOneDmg();
        }
    }

    bool isOnGround()
    {
        int mask = LayerMask.GetMask("Enemy", "Ground");
        float rayLength = obj_SpriteRenderer.bounds.size.y/2 + 0.1f;
        Vector3 origin = transform.position;
        Vector3 direction = Vector2.down;
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, direction, rayLength, mask);
        
        return hitInfo.collider != null;
    }
}
