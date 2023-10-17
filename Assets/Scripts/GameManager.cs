using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private int starsCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("yeah");
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("yo");
        }
    }
}
