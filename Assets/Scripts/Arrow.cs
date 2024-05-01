using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D  Myrigidbody2d;
    [SerializeField] float ArrowSpeed = -1f;
    PlayerMovementScripts player;
    float xSpeed;
    void Start()
    {
        Myrigidbody2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovementScripts>();
        xSpeed = player.transform.localScale.x * ArrowSpeed;
    }

    void Update()
    {
        Myrigidbody2d.velocity = new Vector2(xSpeed, 0f);
//        FlipSprite();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);

        }
        Destroy(gameObject);
        
    }
    void OnCollisionEnter2D(Collision2D other) {Destroy(gameObject); }
    
//    void FlipSprite()
//    {
//        bool HasHorizontalSpeed = Mathf.Abs(Myrigidbody2d.velocity.x) > Mathf.Epsilon;
//
//        if(HasHorizontalSpeed)
//        {
//            transform.localScale = new Vector2 (Mathf.Sign(Myrigidbody2d.velocity.x),1f);
//    
//        }
//    }
}
