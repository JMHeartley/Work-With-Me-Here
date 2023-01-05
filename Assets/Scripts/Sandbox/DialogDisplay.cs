using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    static TextMeshProUGUI _display;

    private void Awake()
    {
        _display = GetComponent<TextMeshProUGUI>();
    }

    public static void ChangeText(string text)
    {
        _display.text = text;
    }
}
