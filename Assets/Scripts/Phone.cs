using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField] GameObject callingSprite;

    [Header("Sounds")]
    [SerializeField] AudioClip ringtone;
    [SerializeField] AudioClip missedCall;
    [SerializeField] AudioClip answeredCall;

    //cached components
    AudioSource audioSource;
    bool isRinging = false;

    Coroutine ringing;

    // Start is called before the first frame update
    void Start()
    {
        Timer.PhoneStartRinging += StartRinging;

        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        Timer.PhoneStartRinging -= StartRinging;
    }

    public void StartRinging()
    {
        ringing = StartCoroutine(Ringing());
    }

    IEnumerator Ringing()
    {
        callingSprite.SetActive(true);

        audioSource.clip = ringtone;
        audioSource.Play();

        isRinging = true;

        yield return new WaitForSeconds(13f);

        if (isRinging)
        {
            CallNotAnswered();
        }
    }

    void StopRinging()
    {
        StopCoroutine(Ringing());

        callingSprite.SetActive(false);

        audioSource.clip = answeredCall;
        audioSource.Play();
    }

    private void OnMouseDown()
    {
        if (isRinging)
        {
            StopRinging();
        }
    }

    void CallNotAnswered()
    {
        callingSprite.SetActive(false);

        isRinging = false;

        audioSource.clip = missedCall;
        audioSource.Play();
    }

}
