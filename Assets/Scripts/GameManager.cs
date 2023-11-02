using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
        // SINGLETON PART
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private int starsCount = 0;
    [SerializeField] private int maxStar;
    public TextMeshProUGUI textTMP;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);   
    }

    
    public void AddStar()
    {
        starsCount ++;
        UpdateStarCount();
        if (starsCount == maxStar)
        {
            EndGame();
        }
    }

    private void UpdateStarCount()
    {
        textTMP.text = starsCount + "/" + maxStar;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("yeah");
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("yo");
        }
    }

    public void EndGame()
    {
        Debug.Log("endgame");
    }
}
