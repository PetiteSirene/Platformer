using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformRaycaster : Raycaster
{

    void Awake()
    {

    }

    
    public override void Raycast(Vector2 vect) 
    {
        int layerMask = 1 << 6; //player is currently on 6
       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vect , vect.magnitude, layerMask);
        if (hit.collider != null)
            {
                PhysicObject po = hit.collider.gameObject.GetComponent<PhysicObject>();
                if (raycastType == RaycastType.X_left)
                {
                    if (vect.x < 0)
                    {                      
                        PhysicSystem.SetSpeedX(po, vect.x/Time.deltaTime);
                        float newX = transform.position.x - po.size + vect.x * Time.deltaTime;
                        PhysicSystem.SetPositionX(po, newX);
                    }
                }
                else if (raycastType == RaycastType.X_right)
                {
                    if (vect.x > 0)
                    {
                        PhysicSystem.SetSpeedX(po, vect.x/Time.deltaTime);
                        float newX = transform.position.x + po.size + vect.x * Time.deltaTime;
                        PhysicSystem.SetPositionX(po, newX);
                    }
                }
                else if (raycastType == RaycastType.Y_down)
                {
                    if (vect.y < 0)
                    {
                        PhysicSystem.SetSpeedY(po, vect.y/Time.deltaTime);
                        float newY = transform.position.y - po.size + vect.x * Time.deltaTime;
                        PhysicSystem.SetPositionY(po, newY);
                    }    
                }
                else //Y.up
                {
                    if (vect.y > 0)
                    {
                        PhysicSystem.SetSpeedY(po, vect.y/Time.deltaTime);
                        float newY = transform.position.y + po.size + vect.x * Time.deltaTime;
                        PhysicSystem.SetPositionY(po, newY);
                    }
                }
                    
            }        
        }
    }

