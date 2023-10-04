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
                PhysicSystem.SetSpeed(po, vect/Time.deltaTime);
                //deplacer le joueur au bon endroit 
                    
            }        
    }


}
