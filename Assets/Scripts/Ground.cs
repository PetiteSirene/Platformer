using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Vector2 info;
    public GroundType groundType;
}

public enum GroundType
    {
        BaseGround,
        Ice,
        Bumper,
        Moving,
    }
