using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popUpPrefab;

    [SerializeField] Canvas parentCanvas;

    string thisObjectsPopUp = "";
    float duration = 0.25f;
    float offset = 1f;
    bool isEnabled;

    void Awake()
    {
        isEnabled = GameSession.MobileSession;

        if (isEnabled) GetPopUpMessage();
        //else Debug.Log("Pop Up Disabled");
    }

    private void OnMouseDown()
    {
        if (isEnabled)
        {
            var go = Instantiate(popUpPrefab, parentCanvas.transform);

            go.text = thisObjectsPopUp;

            Vector3 cursorPosWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            go.transform.position = new Vector3(cursorPosWorldPoint.x, cursorPosWorldPoint.y + offset, go.transform.position.z);

            //offset or animation or some kind

            Destroy(go, duration); 
        }
    }

    void GetPopUpMessage()
    {
        string objectName = gameObject.name;

        if (objectName.Contains("(Clone)"))
        {
            objectName = objectName.Replace("(Clone)", "");
        }

        switch (objectName)
        {
            // COMPUTER OBJECTS
            case "Email (Ghetto Prefab)":
                thisObjectsPopUp = "+1 Responded";
                break;
            case "Document (Ghetto Prefab)":
                thisObjectsPopUp = "+1 Approved";
                break;

            // PHONE
            case "Phone":
            case "Phone Active":
                thisObjectsPopUp = "+1 Answered";
                break;

            // DOG
            case "Dog Playing":
            case "Dog Relaxing":
            case "Dog Suspicious":
                //thisObjectsPopUp = "+" + FindObjectOfType<Dog>().PetFactor + " Love";
                //break;
            case "Dog Sad":
                //thisObjectsPopUp = "+" + FindObjectOfType<Dog>().HappinessMax + " Love";
                //break;
            case "Dog Toy Rope":
                //thisObjectsPopUp = "+" + FindObjectOfType<Dog>().PlayFactor + " Love";
                //break;

                thisObjectsPopUp = "+1 Pet";
                break;

            default:
                thisObjectsPopUp = "Defeault Pop Up Message";
                break;
        }
    }
}
