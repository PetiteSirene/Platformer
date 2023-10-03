using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Ground
{

    public Vector3 position1, position2;

    private bool forwardOrBackward = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((transform.position == position1) || (transform.position == position2))
        {
            forwardOrBackward = !forwardOrBackward;
        }
        
            if (forwardOrBackward)
            {
                transform.position = Vector3.MoveTowards(transform.position, position1, info.y * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, position2, info.y * Time.deltaTime);
            }
        
        
        
    }
}
