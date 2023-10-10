using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    
    [SerializeField] private float smoothSpeed, triggerDistance;
    [SerializeField]private GameObject player;
    private Vector3 offset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x > transform.position.x + triggerDistance)||(player.transform.position.x < transform.position.x - triggerDistance))
        {
            Move();
        }
        
    }

    void Move()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, -10); 
    }
    
    
}
