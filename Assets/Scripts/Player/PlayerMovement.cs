using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform footTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Animator anim;

    private float _moveInput;
    private bool _needJump;

    private void Update()
    {
        if (!IsMyTurn()) return;
        _moveInput = Input.GetAxisRaw( "Horizontal" );
        if (CheckIsGrounded() && (Input.GetKeyDown( KeyCode.UpArrow ) || Input.GetKeyDown( KeyCode.Space )))
        {
            _needJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (!IsMyTurn()) return;
        AdjustAnimation();
        JumpIfNecessary();
        ApplyHorizontalMovement();
    }

    private bool CheckIsGrounded()
    {
        return Physics2D.OverlapCircle( footTransform.position, groundCheckRadius, groundLayer );
    }

    private void ApplyHorizontalMovement()
    {
        rb.velocity = new Vector2( _moveInput * moveSpeed, rb.velocity.y );
    }
    private void AdjustAnimation()
    {
        if (_moveInput <= -0.05f)
        {
            transform.localScale = new Vector3( -1f, transform.localScale.y, transform.localScale.z );
            anim.SetBool( "isMoving", true );
        }

        else if (_moveInput >= 0.05f)
        {
            transform.localScale = new Vector3( 1f, transform.localScale.y, transform.localScale.z );
            anim.SetBool( "isMoving", true );
        }
        else
            anim.SetBool( "isMoving", false );
    }

    private void JumpIfNecessary()
    {
        if (_needJump)
        {
            rb.velocity = new Vector2( rb.velocity.x, jumpForce );
            _needJump = false;
        }
    }

    private bool IsMyTurn()
    {
        return TurnManager.Instance.GetCurrentPlayableCharacter() == transform;
    }
}
