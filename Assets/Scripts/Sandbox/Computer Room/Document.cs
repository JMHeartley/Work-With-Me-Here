using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : MonoBehaviour
{
    private void Awake()
    {
        if (name.Contains("(Clone)")) GetComponent<Transform>().localPosition = new Vector3(0, 0, transform.localPosition.z);
    }

    private void OnMouseDown()
    {
        FindObjectOfType<DocSpawner>().DocClicked(this);        
    }
}
