using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;

public class CubeController : MonoBehaviour
{
    public ParticleGenerator simpleJumpPG;
    public ParticleGenerator doubleJumpPG;
    public ParticleGenerator leftJumpPG;
    public ParticleGenerator rightJumpPG;



    public PhysicObject po;
    
    public float xMoveSpeed, wallslideSpeed, dashSpeed;

    public float baseJumpForce, doubleJumpXForce, doubleJumpYForce, wallJumpXForce, wallJumpYForce;

     
    public float inertieStartOnGround, inertieEndOnGround, inertieOnIce, inertieInAir, inertieWallslide, inertieDash;

    
    private Vector2 inputMove;
    private bool canDoubleJump = true, canDash = true;

   
    public float gravityScale;

    private bool isMoving;
    
    private bool isDashing;
    public float timeDashing;
    
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
                switch(po.groundType)
                    {
                        case GroundType.Ice: 
                            PhysicSystem.TargetSpeedX(po, inputMove.x * xMoveSpeed, inertieOnIce);
                            break;

                        case GroundType.Moving:
                            Vector2 newVect = inputMove.x * xMoveSpeed * Vector2.right + po.groundInfo;
                            if (po.speed.y > po.groundInfo.y)
                            {
                                PhysicSystem.SetSpeedX(po, newVect.x);
                            }
                            else
                            {
                                PhysicSystem.SetSpeed(po, newVect);
                            }
                            break;

                        default:
                            PhysicSystem.TargetSpeedX(po, inputMove.x * xMoveSpeed, inertieStartOnGround);
                            break;

                    }
                canDoubleJump = true;
                canDash = true;
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
                switch(po.groundType)
                    {
                        case GroundType.Ice: 
                            PhysicSystem.TargetSpeedX(po, 0, inertieOnIce);
                            break;

                        case GroundType.Moving:
                            if (po.speed.y > po.groundInfo.y)
                            {
                                PhysicSystem.SetSpeedX(po, po.groundInfo.x);
                            }
                            else
                            {
                                PhysicSystem.SetSpeed(po, po.groundInfo);  
                            }
                            
                            break;

                        default:
                            PhysicSystem.TargetSpeedX(po, 0, inertieEndOnGround);
                            break;

                    }
                canDoubleJump = true;
                canDash = true;
            }
            else
            {
                PhysicSystem.TargetSpeedX(po, 0, inertieInAir);
            }
        }
        
        if (po.isOnLeftWall || po.isOnRightWall)
        {
            canDoubleJump = true;
            canDash = true;
            if (po.speed.y < -wallslideSpeed)
            {
                PhysicSystem.TargetSpeedY(po, -wallslideSpeed, inertieWallslide);
            }
            else
            {
                PhysicSystem.AddSpeedY(po, -gravityScale * Time.deltaTime);
            }
            
            
        }
        else
        {
            PhysicSystem.AddSpeedY(po, -gravityScale * Time.deltaTime);
        }

        if (isDashing)
        {
            PhysicSystem.TargetSpeedX(po, inputMove.x * dashSpeed, inertieDash);
        }
        

    }
    
    //Method appelée en Event par l'InputSystem 
    public void TryJump(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Started )
        {
            if (po.isOnGround)
            {
                simpleJumpPG.PlayVFX();
                PhysicSystem.SetSpeedY(po, baseJumpForce);
            }
            else if(po.isOnLeftWall)
            {
                leftJumpPG.PlayVFX();
                PhysicSystem.SetSpeedX(po, wallJumpXForce);
                PhysicSystem.SetSpeedY(po, wallJumpYForce);
            }
            else if(po.isOnRightWall)
            {
                rightJumpPG.PlayVFX();
                PhysicSystem.SetSpeedX(po, -wallJumpXForce);
                PhysicSystem.SetSpeedY(po, wallJumpYForce);
            }
            else if (canDoubleJump)
            {
                doubleJumpPG.PlayVFX();
                canDoubleJump = false;
                if (isMoving)
                {
                    PhysicSystem.SetSpeedX(po, doubleJumpXForce * inputMove.x);
                }
                PhysicSystem.SetSpeedY(po, doubleJumpYForce);
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

    public void TryDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (canDash)
            {
                StartCoroutine(TimerDash());
            }
            
        }
    }

    private IEnumerator TimerDash()
    {
        isDashing = true;
        canDash = false;
        PhysicSystem.SetSpeedY(po,  0);
        yield return new WaitForSeconds(timeDashing);
        PhysicSystem.SetSpeed(po, new Vector2(inputMove.x * dashSpeed * (1-inertieDash), 0));
        isDashing = false;
    }


}
