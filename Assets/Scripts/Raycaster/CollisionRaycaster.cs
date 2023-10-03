using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionRaycaster : Raycaster
{
    private Vector2 offset;

    void Awake()
    {
        offset = transform.localPosition;
    }

    
    public override void Raycast(Vector2 vect)
    {
        int layerMask = 1 << 7; //level is currently on 7
        if (raycastType == RaycastType.X_left)
        {
            if (vect.x < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.x * Vector2.right , -vect.x, layerMask);
                if (hit.collider != null)
                {
                    PhysicSystem.SetSpeedX(po, 0f);
                    float x = transform.position.x - offset.x - hit.distance;
                    PhysicSystem.SetPositionX(po, x);
                }
            } 
        }
        else if (raycastType == RaycastType.X_right)
        {
            if (vect.x > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.x * Vector2.right ,vect.x, layerMask);
                if (hit.collider != null)
                {
                    PhysicSystem.SetSpeedX(po, 0f);
                    float x = transform.position.x - offset.x + hit.distance;
                    PhysicSystem.SetPositionX(po, x);
                }
            } 
        }
        else if (raycastType == RaycastType.Y_down)
        {
            if (vect.y < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.y * Vector2.up , -vect.y, layerMask);
                Collider2D collider = hit.collider;
                if (collider != null)
                {
                    float y;
                    Ground ground = collider.gameObject.GetComponent<Ground>();
                    switch(ground.groundType)
                    {
                        case GroundType.BaseGround: 
                            po.groundType = GroundType.BaseGround;
                            PhysicSystem.SetSpeedY(po, 0f);
                            y = transform.position.y - offset.y - hit.distance;
                            PhysicSystem.SetPositionY(po, y);
                            break;

                        case GroundType.Ice: 
                            po.groundType = GroundType.Ice;
                            PhysicSystem.SetSpeedY(po, 0f);
                            y = transform.position.y - offset.y - hit.distance;
                            PhysicSystem.SetPositionY(po, y);
                            break;

                        case GroundType.Bumper: 
                            po.groundType = GroundType.Bumper;
                            Debug.Log("boing");
                            PhysicSystem.SetSpeedY(po, Mathf.Min(- vect.y/ Time.deltaTime, ground.info.x));
                            y = transform.position.y - offset.y - hit.distance;
                            PhysicSystem.SetPositionY(po, y);
                            break;

                        case GroundType.Moving: 
                            po.groundType = GroundType.Moving;
                            PhysicSystem.SetSpeedY(po, 0f);
                            y = transform.position.y - offset.y - hit.distance;
                            PhysicSystem.SetPositionY(po, y);
                            break;

                        case GroundType.Lava: 
                            po.groundType = GroundType.Lava;
                            PhysicSystem.SetSpeedY(po, 0f);
                            y = transform.position.y - offset.y - hit.distance;
                            PhysicSystem.SetPositionY(po, y);
                            break;

                        default:
                            PhysicSystem.SetSpeedY(po, 0f);
                            y = transform.position.y - offset.y - hit.distance;
                            PhysicSystem.SetPositionY(po, y);
                            break;

                    }
                }
            }
        }
        else
        {
            if (vect.y > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.y * Vector2.up , vect.y, layerMask);
                if (hit.collider != null)
                {
                    PhysicSystem.SetSpeedY(po, 0f);
                    float y = transform.position.y - offset.y + hit.distance;
                    PhysicSystem.SetPositionY(po, y);
                }
            }
        }     
    }


}
