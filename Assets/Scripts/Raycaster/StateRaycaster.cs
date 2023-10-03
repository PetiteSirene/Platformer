using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRaycaster : Raycaster
{
    public override void Raycast(Vector2 vect)
    {
        int layerMask = 1 << 7; //level is currently on 7
        if (raycastType == RaycastType.X_left)
        {   
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left , vect.x, layerMask);
            if (hit.collider != null)
            {
                po.isOnLeftWall = true;
            }
        }
        else if (raycastType == RaycastType.X_right)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right , vect.x, layerMask);
            if (hit.collider != null)
            {
                po.isOnRightWall = true;  
            }
        } 
        
        else if (raycastType == RaycastType.Y_down)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down , vect.y, layerMask);
            if (hit.collider != null)
            {
                po.isOnGround = true;
            }
            
        }
        else
        {
        } 
    }
}
