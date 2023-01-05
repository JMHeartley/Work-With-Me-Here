using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneActive : MonoBehaviour
{
    Phone phone;
    void Awake()
    {
        phone = GetComponentInParent<Phone>();
    }

    private void OnMouseDown()
    {
        phone.PhoneActiveOnMouseDown();
    }
}
