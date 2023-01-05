using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Router : MonoBehaviour
{
    // diplayed in editor
    [SerializeField] Animator glowAnimator;

    [Header("Cached Game Objects")]
    [SerializeField] GameObject routerNormal;
    [SerializeField] GameObject routerUnplugged;

    // public events
    public delegate void WifiAction();
    public static event WifiAction WifiDisconnected;
    public static event WifiAction WifiReconnected;

    // private variables
    static bool _isPlugged = true;
    int _unplugsTotal = 0;

    Dog dog;

    private void Awake()
    {
        Dog.UnpluggedRouter += UnplugRouter;
        Timer.GameOver += ExportScores;
        RoomChanger.RoomChanged += ToggleGlow;

        dog = FindObjectOfType<Dog>();
    }

    void Start()
    {
        ShowPlugged();
    }

    void OnDestroy()
    {
        Dog.UnpluggedRouter -= UnplugRouter;
        Timer.GameOver -= ExportScores;
        RoomChanger.RoomChanged -= ToggleGlow;
    }
    void OnMouseDown()
    {
        if (dog.IsSad)
        {
            DialogDisplay.ChangeText("I should comfort Toffee first");
        }
        else if (!_isPlugged)
        {
            PlugInRouter();
        }
    }
    void ExportScores() => FindObjectOfType<ScoreManager>().AddUnplugScore(_unplugsTotal);

    void UnplugRouter()
    {
        if (_isPlugged)
        {
            _unplugsTotal++;

            _isPlugged = false;

            ShowUnplugged();

            WifiDisconnected?.Invoke();

            glowAnimator.SetBool("Unplugged", !_isPlugged);
        }
    }

    void PlugInRouter()
    {
        _isPlugged = true;

        ShowPlugged();

        WifiReconnected?.Invoke();

        glowAnimator.SetBool("Unplugged", !_isPlugged);
    }

    #region On Screen Logic
    void ShowPlugged()
    {
        routerNormal.SetActive(true);
        routerUnplugged.SetActive(false);
    }

    void ShowUnplugged()
    {
        routerNormal.SetActive(false);
        routerUnplugged.SetActive(true);

        ToggleGlow();
    }

    void ToggleGlow()
    {
        if (transform.parent.localPosition.x == 25)
        {
            glowAnimator.SetBool("Offscreen", true);
            //turn off when offscreen
        }
        else if (transform.parent.localPosition.x == 0)
        {
            glowAnimator.SetBool("Offscreen", false);
        }

    }
    #endregion
}