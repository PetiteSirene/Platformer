using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
        // SINGLETON PART
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    public AudioSource startCollect;

    private int starsCount = 0;
    [SerializeField] private int maxStar;
    public TextMeshProUGUI textTMPStartCount, textTMPTimer, TextTMPTimerEndMenu;

    private float timer;
    
    private bool paused = false, victory;
    
    public GameObject menu, player;
    private Vector3 playerPositionInit;

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
        playerPositionInit = player.transform.position;
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
            if(victory)
                return;
            Time.timeScale = 1;
            menu.transform.GetChild(0).gameObject.SetActive(false);
        }

        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        textTMPTimer.text = niceTime;
        TextTMPTimerEndMenu.text = "Timer : " + niceTime;
    }


    public void AddStar()
    {
        starsCount ++;
        startCollect.Play();
        UpdateStarCount();
        if (starsCount == maxStar)
        {
            EndGame();
        }
    }

    private void UpdateStarCount()
    {
        textTMPStartCount.text = starsCount + "/" + maxStar;
    }

    public void Esc(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (victory)
            {
                Time.timeScale = 1;
                menu.transform.GetChild(1).gameObject.SetActive(false);
                victory = false;
                starsCount = 0;
                UpdateStarCount();
                player.transform.position = playerPositionInit;
                timer = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
            paused = !paused;
            
        }
    }

    public void Quit(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
            
        }
    }

    public void EndGame()
    {
        player.GetComponent<CubeController>().po.speed = Vector2.zero;
        player.GetComponent<CubeController>().cameraShake.canShake = false;
        menu.transform.GetChild(1).gameObject.SetActive(true);
        Time.timeScale = 0;
        victory = true;
    }
}
