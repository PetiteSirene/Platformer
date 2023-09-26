using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float gravityForce, speedMax;
    
    private Vector2 move, inputMove;
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move + inputMove);
        Gravity();
    }

    private void Movement()
    {
        
    }
    
    private void Gravity()
    {
        if (!isGrounded)
        {
            move = Vector2.down * gravityForce;
        }
        else
        {
            move = Vector2.zero;
        }

    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            
        }
        if (context.phase == InputActionPhase.Performed)
        {
            inputMove = context.ReadValue<Vector2>();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            inputMove = Vector2.zero;
        }

        inputMove *= speedMax;

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        isGrounded = true;
    }
}
