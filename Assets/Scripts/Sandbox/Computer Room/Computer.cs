using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Computer : MonoBehaviour
{
    // diplayed in editor
    [SerializeField] int offScreenPosition = 25;

    [Header("Screens")]
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject noConnectScreen;
    [SerializeField] GameObject reConnectScreen;

    [Space(10)]
    [SerializeField] int reConnectionDelay = 5;

    [Space(10)]
    [SerializeField] TextMeshProUGUI dialogDisplay;

    [SerializeField] GameObject roomCanvas;

    public static bool HasRouterIssues { get; private set; }

    private void Start()
    {
        // subscribe to events
        RoomChanger.RoomChanged += ToggleMainCanvases;
        Router.WifiDisconnected += DisplayNoConnect;
        Router.WifiReconnected += DisplayReConnect;

        DisplayMain();
    }

    private void OnDestroy()
    {
        // unsubscribe from events
        RoomChanger.RoomChanged -= ToggleMainCanvases;
        Router.WifiDisconnected -= DisplayNoConnect;
        Router.WifiReconnected -= DisplayReConnect;
    }

    #region Display Methods
    public void DisplayMain()
    {
        ShowMain();

        DialogDisplay.ChangeText("");

        HasRouterIssues = false;

        string activeRoom = FindObjectOfType<RoomChanger>().ActiveRoomName;

        if (activeRoom.Equals("Computer Room"))
        {
            ActivateMainScreenCanvases();
        }

        noConnectScreen.SetActive(false);
        reConnectScreen.SetActive(false);
    }
    public void DisplayNoConnect()
    {
        HideMain();

        HasRouterIssues = true;
        DeactivateMainScreenCanvases();

        noConnectScreen.SetActive(true);
        reConnectScreen.SetActive(false);

        DialogDisplay.ChangeText("I need to plug the router back in! Darn Toffee, that little scamp...");
    }

    public void DisplayReConnect()
    {
        noConnectScreen.SetActive(false);
        reConnectScreen.SetActive(true);

        StartCoroutine(ReconnnectingDelay());
    }
    #endregion

    #region Reconnection Logic
    IEnumerator ReconnnectingDelay()
    {
        DialogDisplay.ChangeText("Okay, the router's plugged back in, now it needs to reconnect...");

        //Debug.Log("starting...");
        yield return new WaitForSeconds(reConnectionDelay);
        //Debug.Log("ended!");

        DisplayMain();
    }
    private void ShowMain() => mainScreen.transform.position = new Vector3(mainScreen.transform.position.x,
                                                                           0f,
                                                                           mainScreen.transform.position.z);

    private void HideMain() => mainScreen.transform.position = new Vector3(mainScreen.transform.position.x,
                                                                       offScreenPosition,
                                                                       mainScreen.transform.position.z);
    #endregion

    #region Canvas Logic
    private void ToggleMainCanvases()
    {
        //Debug.Log("ToggleCanvases");
        if (transform.parent.localPosition.x == 25)
        {
            DeactivateMainScreenCanvases();
        }
        else if (transform.parent.localPosition.x == 0 && !HasRouterIssues)
        {
            ActivateMainScreenCanvases();
        }
    }

    private void ActivateMainScreenCanvases()
    {
        roomCanvas.SetActive(true);

        //foreach (GameObject canvas in canvases)
        //{
        //    canvas.SetActive(true);
        //}
    }

    private void DeactivateMainScreenCanvases()
    {
        roomCanvas.SetActive(false);

        //foreach (GameObject canvas in canvases)
        //{
        //    canvas.SetActive(false);
        //}
    }
    #endregion
}