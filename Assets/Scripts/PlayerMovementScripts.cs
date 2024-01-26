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
    [SerializeField] float JumpSpeed =1f;
    [SerializeField] float ClimbSpeed =1f;
    Animator MyAnimator;
    CapsuleCollider2D myCapsuleCollider;
    float MyGravityScaleAtStart;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        MyGravityScaleAtStart = MyRigidbody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();

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
        
        
        bool HasHorizontalSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        MyAnimator.SetBool("isRunning", HasHorizontalSpeed);
    }

    void OnJump(InputValue value)
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            MyRigidbody.velocity += new Vector2 (0f, JumpSpeed);
        }
    }


    void FlipSprite()
    {
        bool HasHorizontalSpeed = Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;

        if(HasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(MyRigidbody.velocity.x),1f);
        }
    }
    void ClimbLadder()
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            MyRigidbody.gravityScale = MyGravityScaleAtStart;
            return;
        }
        Vector2 ClimbVelocity = new Vector2(MyRigidbody.velocity.x,moveInput.y * ClimbSpeed);
        MyRigidbody.velocity = ClimbVelocity;
        MyRigidbody.gravityScale =0f;
    }
}
