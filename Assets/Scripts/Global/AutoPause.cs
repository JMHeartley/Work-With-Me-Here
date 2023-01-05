using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPause : MonoBehaviour
{
    [SerializeField] GameObject pauseOverlay;

    // public events
    public delegate void GameAction();
    public static event GameAction GamePaused;
    public static event GameAction GameUnpaused;

    bool isPaused = false;
    bool debugEnabled;

    private void Start()
    {
        debugEnabled = FindObjectOfType<GameSession>().disablePause;

        pauseOverlay.SetActive(false);
    }

    void Update()
    {
        if (!debugEnabled)
        {
            if (isPaused) PauseGame();
            else ResumeGame();
        }
    }

    void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;

            //Debug.Log("Paused");

            pauseOverlay.SetActive(true);

            GamePaused?.Invoke();
        }
    }

    void ResumeGame()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;

            //Debug.Log("Unpaused");

            pauseOverlay.SetActive(false);

            GameUnpaused?.Invoke();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }
}
