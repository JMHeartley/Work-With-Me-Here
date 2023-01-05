using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class PressHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] string url;

    public delegate void SocialAction();
    public static event SocialAction SharetoSocial;

    public void OnPointerDown(PointerEventData eventData)
    {
        Link.url = url;

        SharetoSocial?.Invoke();
    }
}