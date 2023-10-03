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
    public float xMoveSpeed, wallslideSpeed;

    public float baseJumpForce, doubleJumpForce, wallJumpXForce, wallJumpYForce;
    
    private Vector2 inputMove;
    public float inertieOnGround, inertieInAir, inertieWallslide;

    public float gravityScale;

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
        bool isOnGround = po.isOnGround;

        
        if (isMoving)
        {
            if (isOnGround)
            {
                PhysicSystem.TargetSpeedX(po, inputMove.x * xMoveSpeed, inertieOnGround);
            }
            else
            {
                PhysicSystem.TargetSpeedX(po, inputMove.x * xMoveSpeed, inertieInAir);
            }

        }
        else
        {
            if (isOnGround)
            {
                PhysicSystem.TargetSpeedX(po, 0, inertieOnGround);
            }
            else
            {
                PhysicSystem.TargetSpeedX(po, 0, inertieInAir);
            }
        }
        if (po.speed.y < -wallslideSpeed && (po.isOnLeftWall || po.isOnRightWall))
        {
            PhysicSystem.TargetSpeedY(po, -wallslideSpeed, inertieWallslide);
        }
        else
        {
            PhysicSystem.AddSpeedY(po, -gravityScale * Time.deltaTime);
        }
        

    }
    
    //Method appelée en Event par l'InputSystem 
    public void TryJump(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Started )
        {
            if (po.isOnGround)
            {
                PhysicSystem.SetSpeedY(po, baseJumpForce);
            }
            else if(po.isOnLeftWall)
            {
                PhysicSystem.SetSpeedX(po, wallJumpXForce);
                PhysicSystem.SetSpeedY(po, wallJumpYForce);
            }
            else if(po.isOnRightWall)
            {
                PhysicSystem.SetSpeedX(po, -wallJumpXForce);
                PhysicSystem.SetSpeedY(po, wallJumpYForce);
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
