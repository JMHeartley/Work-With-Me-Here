using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour
{
    public static string url;

    void OnAwake()
    {
        PressHandler.SharetoSocial += OpenLinkJSPlugin;
    }

    void OnDestroy()
    {
        PressHandler.SharetoSocial -= OpenLinkJSPlugin;
    }

    public void OpenLinkJSPlugin()
    {
#if !UNITY_EDITOR
		openWindow(url);
#endif
        Debug.Log("Opening " + url);
    }

    [DllImport("__Internal")] private static extern void openWindow(string url);
}