using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;

public class CubeController : MonoBehaviour
{
    public PhysicObject po;
    public float speedMax, jumpForce;
    
    private Vector2 inputMove;
    public float inertieOnGround, inertieOnAir;

    private bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        po = GetComponent<PhysicObject>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving)
        {
            if (po.isOnGround)
            {
                PhysicSystem.TargetSpeedX(po, inputMove.x * speedMax, inertieOnGround);
            }
            else
            {
                PhysicSystem.TargetSpeedX(po, inputMove.x * speedMax, inertieOnAir);
            }

        }
        else
        {
            if (po.isOnGround)
            {
                PhysicSystem.TargetSpeedX(po, 0, inertieOnGround);
            }
            else
            {
                PhysicSystem.TargetSpeedX(po, 0, inertieOnAir);
            }
        }

    }
    
    //Method appelée en Event par l'InputSystem 
    public void Jump(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Started )
        {
            if (po.isOnGround)
            {
                PhysicSystem.SetSpeedY(po, jumpForce);
                po.isOnGround = false;
            }
            

        }
        
    }
    
    
    //Method appelée en Event par l'InputSystem 
    public void Move(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
        if (context.phase == InputActionPhase.Started)
        {
            isMoving = true;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isMoving = false;
        }
    }

    
}
