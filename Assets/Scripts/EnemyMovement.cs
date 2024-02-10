using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody2d;

    void Start()
    {
       myRigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidbody2d.velocity = new Vector2(moveSpeed,0f);

    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        flipFacingEnemy();
    
    }
    void flipFacingEnemy()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody2d.velocity.x)),1f);
    }
}
