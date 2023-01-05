using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class DebugText : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string IsMobile();

    private void Start()
    {
        IsMobile();
    }
    public void Debug(string onMobile)
    {
        var isRunningOnMobile = bool.Parse(onMobile);

        var debugText = GetComponent<TextMeshProUGUI>();

        debugText.text = "Running on mobile: " + isRunningOnMobile;
    }

}
