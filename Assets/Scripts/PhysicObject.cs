using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    public List<Raycaster> raycasters;
    public float gravityScale;
    public Vector2 speed;

    void Start()
    {
        
    }

    void Update()
    {
        speed += Vector2.down * gravityScale * Time.deltaTime;
        foreach (Raycaster raycaster in raycasters)
        {
            if (speed != Vector2.zero)
            {
                raycaster.Raycast(speed * Time.deltaTime);
            }
        }       
    }
    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(speed * Time.deltaTime);
    }

}
