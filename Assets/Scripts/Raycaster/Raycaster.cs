using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Raycaster : MonoBehaviour
{
    public PhysicObject po;
    public RaycastType raycastType;


    public abstract void Raycast(Vector2 vect);
    
}



public enum RaycastType
    {
        X_left,
        X_right,
        Y_down,
        Y_up,
    }
