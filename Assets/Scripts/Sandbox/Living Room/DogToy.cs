using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogToy : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<Dog>().PlayWithDog();
    }
}
