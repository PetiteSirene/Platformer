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
    public ParticleGenerator leftElectricityPG;
    public ParticleGenerator rightElectricityPG;

    public PhysicObject po;
    public CameraShake cameraShake;
    
    public float xMoveSpeed, wallslideSpeed, dashSpeed;

    public float baseJumpForce, doubleJumpXForce, doubleJumpYForce, wallJumpXForce, wallJumpYForce;

     
    public float inertieStartOnGround, inertieEndOnGround, inertieOnIce, inertieInAir, inertieWallslide, inertieDash;

    
    private Vector2 inputMove;
    private bool canDoubleJump = true;
    public bool canDash = true;

   
    public float gravityScale;

    private bool isMoving;
    
    public bool isDashing;
    public float dashDuration;

    public GameObject menu;
    
    // Start is called before the first frame update
    void Start()
    {
        po = GetComponent<PhysicObject>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        if (paused)
        {
            menu.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        Time.timeScale = 1;
        menu.transform.GetChild(0).gameObject.SetActive(false);
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
                if (inputMove.x > 0.1)
                {
                    cameraShake.ShakeCamera();
                    StartCoroutine(TimerDash(true));
                }
                else if (inputMove.x < -0.1)
                {
                    cameraShake.ShakeCamera();
                    StartCoroutine(TimerDash(false));
                }     
            }
        }
    }

    private IEnumerator TimerDash(bool toRight)
    {
        isDashing = true;
        canDash = false;
        float dashInput;
        Vector2 initialSpeed = po.speed;
        if (toRight)
        {
            rightElectricityPG.PlayVFX();
            dashInput = 1.0f;
        }
        else
        {
            leftElectricityPG.PlayVFX();
            dashInput = - 1.0f;
        }
        PhysicSystem.SetSpeedY(po,  0);
        float timeElapsed = 0;
        while (timeElapsed < dashDuration)
        {
            timeElapsed += Time.deltaTime;
            PhysicSystem.SetSpeed(po, new Vector2(dashInput * dashSpeed, 0));
            yield return null;
        }
        PhysicSystem.TargetSpeedX(po, 0, inertieDash);
        isDashing = false;
    }

    private bool paused =false;

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            paused = !paused;
            
        }
    }
}
