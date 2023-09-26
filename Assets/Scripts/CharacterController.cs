using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;

public class CharacterController : MonoBehaviour
{
    public float gravityForce, speedMax, jumpForce;
    
    private Vector2 gravityMove, inputMove, jumpMove;
    private bool isGrounded, canJump;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(gravityMove + inputMove + jumpMove);
        Gravity();
    }

    private void Gravity()
    {
        if (!isGrounded)
        {
            gravityMove = Vector2.down * gravityForce;
        }
        else
        {
            gravityMove = Vector2.zero;
        }
    }

    //Method appelée en Event par l'InputSystem 
    public void Jump(InputAction.CallbackContext context)
    {
        // if (context.phase == InputActionPhase.Started & canJump)
        //     jumpMove.y = context.ReadValue<float>() * jumpForce;
        //
        // if (context.phase == InputActionPhase.Canceled)
        // {
        //     jumpMove.y = 0;
        // }
    }
    
    
    //Method appelée en Event par l'InputSystem 
    public void Move(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            
        }
        if (context.phase == InputActionPhase.Performed)
        {
            inputMove.x = context.ReadValue<Vector2>().x;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            inputMove.x = 0;
        }

        inputMove *= speedMax;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        canJump = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
        canJump = false;
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        isGrounded = true;
    }
}
