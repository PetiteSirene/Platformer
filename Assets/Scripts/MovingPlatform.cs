using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Ground
{

    public Vector3 position1, position2;

    private bool forwardOrBackward = false;

    public float speed;

    private Vector2 vecteurVitesse;
    // Start is called before the first frame update
    void Start()
    {
        vecteurVitesse = position2 - position1;
        info = vecteurVitesse.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {

        if ((transform.position == position1) || (transform.position == position2))
        {
            forwardOrBackward = !forwardOrBackward;
            info = -info;
        }
        
        if (forwardOrBackward)
        {
            transform.position = Vector3.MoveTowards(transform.position, position1, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, position2, speed * Time.deltaTime);
        }
            
        
        
        
    }
}
