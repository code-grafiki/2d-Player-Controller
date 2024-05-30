using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float MyGravityScaleAtStart;
    bool isAlive = true;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        MyGravityScaleAtStart = MyRigidbody.gravityScale;
    }

    void Update()
    {
        if(!isAlive){ return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if(!isAlive){ return; }
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
        if(!isAlive){ return; }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            MyRigidbody.velocity += new Vector2 (0f, JumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if(!isAlive){ return; }
        Instantiate(arrow, bow.position, transform.rotation);

        if(Input.GetMouseButton(0))
        {
            MyAnimator.SetBool("isShooting",true);
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
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            MyRigidbody.gravityScale = MyGravityScaleAtStart;
            MyAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 ClimbVelocity = new Vector2(MyRigidbody.velocity.x,moveInput.y * ClimbSpeed);
        MyRigidbody.velocity = ClimbVelocity;
        MyRigidbody.gravityScale =0f;

        bool HasVerticalSpeed = Mathf.Abs(MyRigidbody.velocity.y) > Mathf.Epsilon;
        MyAnimator.SetBool("isClimbing", HasVerticalSpeed);
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            isAlive = false;
            MyAnimator.SetTrigger("Dead");
            MyRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().processPlayerDeath();
        }
    }
}