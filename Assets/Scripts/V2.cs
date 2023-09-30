using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class V2 : MonoBehaviour
{
    public float gravityForce, jumpForce;
    private Vector2 speed, gravity, jump;
    private bool isGrounded, canJump;
    
    private void Update()
    {
        Gravity();
        transform.Translate((speed + gravity + jump) * Time.deltaTime);
    }

    //Method appelée en Event par l'InputSystem 
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            jump = Vector2.up * jumpForce;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            jump = Vector2.zero;
        }
    }
    
    
    private void Gravity()
    {
        gravity = Vector2.down * gravityForce;
    }

    public float speedMax;
    
    //Method appelée en Event par l'InputSystem 
    public void Move(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            
        }
        if (context.phase == InputActionPhase.Performed)
        {
            speed.x = context.ReadValue<Vector2>().x * speedMax;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            speed.x = 0;
        }
    }
    
    
}
