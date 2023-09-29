using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicSystem
{

    public static void SetPosition(PhysicObject po, Vector2 position)
    {
        po.transform.position = position;
    }

    public static void SetPositionX(PhysicObject po, float positionX)
    {
        Vector2 temp = new Vector2(positionX, po.transform.position.y);
        po.transform.position = temp;
    }

    public static void SetPositionY(PhysicObject po, float positionY)
    {
        Vector2 temp = new Vector2(po.transform.position.x, positionY);
        po.transform.position = temp;
    }

    public static void SetSpeed(PhysicObject po, Vector2 speed)
    {
        po.speed = speed;
    }
    
    public static void SetSpeedX(PhysicObject po, float speedX)
    {
        po.speed.x = speedX;
    }

    public static void SetSpeedY(PhysicObject po, float speedY)
    {
        po.speed.y = speedY;
    }

    public static void AddSpeed(PhysicObject po, Vector2 speed)
    {
        po.speed += speed;
    }

    public static void AddSpeedX(PhysicObject po, float speedX)
    {
        po.speed.x += speedX;
    }

    public static void AddSpeedY(PhysicObject po, float speedY)
    {
       po.speed.y += speedY;
    }

    public static void TargetSpeed(PhysicObject po, Vector2 speed, float inertie) //inertie entre 0 et 1
    {
        po.speed = (1 - inertie) * speed + inertie * po.speed;
    }

    public static void TargetSpeedX(PhysicObject po,float speedX, float inertie) //inertie entre 0 et 1
    {
        po.speed.x = (1 - inertie) * speedX + inertie * po.speed.x;
    }

    public static void TargetSpeedY(PhysicObject po, float speedY, float inertie) //inertie entre 0 et 1
    {
        po.speed.y = (1 - inertie) * speedY + inertie * po.speed.y;
    }
}
