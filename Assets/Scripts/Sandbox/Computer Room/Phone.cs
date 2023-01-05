using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    // diplayed in editor
    [SerializeField] int offScreenPosition = 15;
    [SerializeField] GameObject activeScreen;

    [Header("Sounds")]
    [SerializeField] AudioClip ringtone;
    [SerializeField] AudioClip missedCall;
    [SerializeField] AudioClip answeredCall;

    [Header("Configurable Params")]
    [SerializeField] float ringDuration = 13f;


    bool isRinging = false;

    int callsTotal = 0;
    int callsAnswered = 0;

    AudioSource audioSource;
    Animator phoneAnimator;

    private void Awake()
    {
        Timer.GameOver += ExportScores;
        AutoPause.GamePaused += PauseRingtone;
        AutoPause.GameUnpaused += UnpauseRingtone;

        audioSource = GetComponent<AudioSource>();
        phoneAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        HideActiveScreen();

        StartCoroutine(PhoneRingCountdown());
    }

    private void OnDestroy()
    {
        Timer.GameOver -= ExportScores;
        AutoPause.GamePaused -= PauseRingtone;
        AutoPause.GameUnpaused -= UnpauseRingtone;
    }

    void PauseRingtone()
    {
        if (audioSource.isPlaying) audioSource.Pause();
    }

    void UnpauseRingtone()
    {
        audioSource.UnPause();
    }

    public void PhoneActiveOnMouseDown()
    {
        if (isRinging) CallAnswered();
    }
    private void ExportScores() => FindObjectOfType<ScoreManager>().AddPhoneScore(callsAnswered, callsTotal);

    #region Phone Cycle Coroutines
    IEnumerator PhoneRingCountdown()
    {
        int x = 7 + Random.Range(0, 8);

        yield return new WaitForSeconds(x);

        StartCoroutine(Ringing());

        StartCoroutine(PhoneDelay());
    }

    IEnumerator PhoneDelay()
    {
        yield return new WaitForSeconds(14f);

        StartCoroutine(PhoneRingCountdown());
    }
    #endregion

    #region Phone Interaction Logic
    IEnumerator Ringing()
    {
        callsTotal++;

        ShowActiveScreen();

        audioSource.clip = ringtone;
        audioSource.Play();

        isRinging = true;

        phoneAnimator.SetBool("Ringing", true);

        yield return new WaitForSeconds(ringDuration);

        if (isRinging)
        {
            CallNotAnswered();
        }

        phoneAnimator.SetBool("Ringing", false);
    }

    private void CallAnswered()
    {
        callsAnswered++;

        StopCoroutine(Ringing());

        HideActiveScreen();

        isRinging = false;

        audioSource.clip = answeredCall;
        audioSource.Play();

        phoneAnimator.SetBool("Ringing", false);
    }


    private void CallNotAnswered()
    {
        HideActiveScreen();

        isRinging = false;

        audioSource.clip = missedCall;
        audioSource.Play();
    }
    #endregion

    #region Active Screen Logic
    private void HideActiveScreen()
    {
        //activeScreen.SetActive(false);
        activeScreen.transform.localPosition = new Vector3(activeScreen.transform.localPosition.x,
                                                        offScreenPosition,
                                                        activeScreen.transform.localPosition.z);
    }

    private void ShowActiveScreen()
    {
        //activeScreen.SetActive(true);
        activeScreen.transform.localPosition = new Vector3(activeScreen.transform.localPosition.x,
                                                        0,
                                                        activeScreen.transform.localPosition.z);
    }
    #endregion

}