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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.x * Vector2.right , Math.Abs(vect.x), layerMask);
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.x * Vector2.right , Math.Abs(vect.x), layerMask);
                if (hit.collider != null)
                {
                    PhysicSystem.SetSpeedX(po, 0f);
                    float x = transform.position.x - offset.x - hit.distance;
                    PhysicSystem.SetPositionX(po, x);
                }
            } 
        }
        else if (raycastType == RaycastType.Y_down)
        {
            if (vect.y < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.y * Vector2.up , Math.Abs(vect.y), layerMask);
                if (hit.collider != null)
                {
                    po.isOnGround = true;
                    PhysicSystem.SetSpeedY(po, 0f);
                    float y = transform.position.y - offset.y - hit.distance;
                    PhysicSystem.SetPositionY(po, y);
                }
            }
        }
        else
        {
            if (vect.y > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vect.y * Vector2.up , Math.Abs(vect.y), layerMask);
                if (hit.collider != null)
                {
                    PhysicSystem.SetSpeedY(po, 0f);
                    float y = transform.position.y - offset.y - hit.distance;
                    PhysicSystem.SetPositionY(po, y);
                }
            }
        }     
    }


}
