using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicSystem
{
    
    public static void AddSpeed(PhysicObject po, Vector2 speed)
    {
        po.speed += speed;
    }

    public static void TargetSpeed(PhysicObject po, Vector2 speed, float inertie) //inertie entre 0 et 1
    {
        po.speed = (1 - inertie) * speed + inertie * po.speed;
    }



}
