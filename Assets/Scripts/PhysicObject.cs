using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    public Vector2 speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime);     
    }
}
