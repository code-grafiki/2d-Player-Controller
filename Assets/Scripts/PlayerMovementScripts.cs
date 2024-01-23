using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScripts : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D MyRigidbody;
    [SerializeField] float PlayerSpeed = 1f;

    Animator MyAnimator;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 PlayerVelocity = new Vector2(moveInput.x*PlayerSpeed, MyRigidbody.velocity.y);
        MyRigidbody.velocity = PlayerVelocity;
        
        MyAnimator.SetBool("IsRunning", true);
    }

    void FlipSprite()
    {
        bool HasHorizontalSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;

        if(HasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(MyRigidbody.velocity.x),1f);
        }
        
    }
}