using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    public List<CollisionRaycaster> cRaycasters;
    public List<StateRaycaster> sRaycasters;
    public Vector2 speed;

    public Vector2 detectionVector;

    public bool isOnGround;
    public bool isOnLeftWall;
    public bool isOnRightWall;
    public GroundType groundType;

    void Start()
    {
        
    }

    void Update()
    {
        DoCRaycasts();
        DoSRaycasts();
        Move();
    }

    void DoCRaycasts()
    {
        foreach (Raycaster cRaycaster in cRaycasters)
        {
            if (speed != Vector2.zero)
            {
                cRaycaster.Raycast(speed * Time.deltaTime);
            }
        }
    }

    void DoSRaycasts()
    {
        ResetBools();
        foreach (Raycaster sRaycaster in sRaycasters)
        {
            sRaycaster.Raycast(detectionVector);
        }

    }

    void ResetBools()
    {
        isOnGround = false;
        isOnLeftWall = false;
        isOnRightWall = false;
    }



    void Move()
    {
        transform.Translate(speed * Time.deltaTime);
    }

}
