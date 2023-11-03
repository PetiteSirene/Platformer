using System;
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
    
    private bool paused = false;
    
    public GameObject menu;
    
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

    private void Update()
    {
        if (paused)
        {
            menu.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            menu.transform.GetChild(0).gameObject.SetActive(false);
        }
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
        if (context.phase == InputActionPhase.Started)
        {
            paused = !paused;
            
        }
    }

    public void Quit(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
            
        }
    }

    public void EndGame()
    {
        Debug.Log("endgame");
    }
}
