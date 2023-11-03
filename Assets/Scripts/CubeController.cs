using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    public BurstPG simpleJumpPG;
    public BurstPG doubleJumpPG;
    public BurstPG leftJumpPG;
    public BurstPG rightJumpPG;
    public ContinuousPG electricityPG;

    public PhysicObject po;
    public CameraShake cameraShake;
    public AudioSource jump, land, dash;

    public float intensityModifierDuringDash = 1.5f;
    
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

    

    private Renderer rend;
    private Color initialColor;
    private Color maxColor;
    
    // Start is called before the first frame update
    void Start()
    {
        po = GetComponent<PhysicObject>();
        Application.targetFrameRate = 60;
        rend = GetComponent<Renderer>();
        initialColor = rend.material.GetColor("_EmissionColor");
        maxColor = initialColor * intensityModifierDuringDash;
        electricityPG.PauseVFX();

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
                jump.Play();
                simpleJumpPG.PlayVFX();
                PhysicSystem.SetSpeedY(po, baseJumpForce);
            }
            else if(po.isOnLeftWall)
            {
                jump.Play();
                leftJumpPG.PlayVFX();
                PhysicSystem.SetSpeedX(po, wallJumpXForce);
                PhysicSystem.SetSpeedY(po, wallJumpYForce);
            }
            else if(po.isOnRightWall)
            {
                jump.Play();
                rightJumpPG.PlayVFX();
                PhysicSystem.SetSpeedX(po, -wallJumpXForce);
                PhysicSystem.SetSpeedY(po, wallJumpYForce);
            }
            else if (canDoubleJump)
            {
                jump.Play();
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
                dash.Play();
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
        Vector2 initialSpeed = po.speed; 
        float dashInput;
        electricityPG.PlayVFX();
        if (toRight)
        {
            
            dashInput = 1.0f;
        }
        else
        {
            dashInput = - 1.0f;
        }

        PhysicSystem.SetSpeedY(po,  0);
        float t = 0;
        float t2;
        Color lerpedColor;
        while (t < dashDuration)
        {
            t += Time.deltaTime;
            t2 = -t * (t - dashDuration) * (4.0f/(dashDuration * dashDuration));
            lerpedColor = Color.Lerp(initialColor, maxColor, t2);
            rend.material.SetColor("_EmissionColor", lerpedColor);
            PhysicSystem.SetSpeed(po, new Vector2(dashInput * dashSpeed, 0));
            yield return null;
        }
        rend.material.SetColor("_EmissionColor", initialColor);
        isDashing = false;
        electricityPG.PauseVFX();
        PhysicSystem.TargetSpeedX(po, 0, inertieDash);
    }

    
}
