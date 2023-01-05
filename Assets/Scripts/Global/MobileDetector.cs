using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class MobileDetector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText;
    [SerializeField] bool debugIsMobile;

    [DllImport("__Internal")]
    private static extern string IsMobile();

    private void Awake()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        IsMobile();
        MakeDebugTextDisappear();
#elif UNITY_EDITOR
        DetectPlatform(debugIsMobile.ToString());
        #endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetButtonDown("Debug 2")) // tab
        {
            if (debugText.color.a == 0) MakeDebugTextAppear();
            else MakeDebugTextDisappear();
        }
#endif
    }
    public void DetectPlatform(string onMobile)
    {
        var isRunningOnMobile = bool.Parse(onMobile);

        debugText.text = "Running on mobile: " + isRunningOnMobile;
        
        GameSession.MobileSession = isRunningOnMobile;

        Debug.Log("Mobile Detector: " + isRunningOnMobile);
    }

    void MakeDebugTextDisappear()
    {
        debugText.color = new Color(
            debugText.color.r,
            debugText.color.g,
            debugText.color.b,
            0);
    }

    void MakeDebugTextAppear()
    {
        debugText.color = new Color(
            debugText.color.r,
            debugText.color.g,
            debugText.color.b,
            1);
    }
}
