using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Email : MonoBehaviour
{
<<<<<<< Updated upstream:Assets/Scripts/Computer Room/Email.cs
=======
    private void Awake()
    {
        if (name.Contains("(Clone)")) GetComponent<Transform>().localPosition = new Vector3(0, 0, transform.localPosition.z);
    }
>>>>>>> Stashed changes:Assets/Scripts/Sandbox/Computer Room/Email.cs

    private void OnMouseDown()
    {
        FindObjectOfType<EmailSpawner>().EmailClicked(this);
    }
}