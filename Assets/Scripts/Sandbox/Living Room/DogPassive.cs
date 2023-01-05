using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class declaraions
[RequireComponent(typeof(BoxCollider2D))]

public class DogPassive : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<Dog>().PetDog();
    }
}
