using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogComfort : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<Dog>().ComfortDog();
    }
}
