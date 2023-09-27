using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;

public class CharacterControllerAriel : MonoBehaviour
{
    public PhysicObject po;
    public float gravityForce, speedMax, jumpForce;
    
    private Vector2 gravityMove, inputMove, jumpMove;
    private bool isGrounded, canJump;
    public float inertie;
    
    // Start is called before the first frame update
    void Start()
    {
        po = GetComponent<PhysicObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
    }

    private void Gravity()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        
        // if (!isGrounded)
        // {
        //     gravityMove = Vector2.down * gravityForce;
        // }
        // else
        // {
        //     gravityMove = Vector2.zero;
        // }
    }

    //Method appelée en Event par l'InputSystem 
    public void Jump(InputAction.CallbackContext context)
    {
        
    }
    
    
    //Method appelée en Event par l'InputSystem 
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(1);
        if (context.phase == InputActionPhase.Started)
        {
            
        }
        if (context.phase == InputActionPhase.Performed)
        {
            PhysicSystem.TargetSpeed(po, speedMax*context.ReadValue<Vector2>(), inertie);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            po.speed = Vector2.zero;
        }

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
