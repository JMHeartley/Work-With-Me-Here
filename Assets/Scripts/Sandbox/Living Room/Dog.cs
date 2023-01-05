using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    // diplayed in editor
    [Header("Dog State Objects")]
    [SerializeField] GameObject dogPlaying;
    [SerializeField] GameObject dogRelaxing;
    [SerializeField] GameObject dogSad;
    [SerializeField] GameObject dogSleeping;
    [SerializeField] GameObject dogSuspicious;

    [Header("Dog Toy Objects")]
    [SerializeField] GameObject dogToyRope;
    [SerializeField] TextMeshProUGUI toyMessage;

    [Header("Dog UI")]
    [SerializeField] Slider dogSlider;
    [SerializeField] Animator glowAnimator;

    [Header("Happiness")]
    [SerializeField] int dogHappinessMax = 20;
    [SerializeField] int dogHappiness = 0;
    [SerializeField] int petFactor = 5;
    [SerializeField] int playFactor = 8;

    // public events
    public delegate void RouterAction();
    public static event RouterAction UnpluggedRouter;

    int dogStateTimerMax = 0;
    int petsTotal;

    bool isDebugEnabled;
    bool isFirstGame;

    RoomChanger roomChanger;

    public int PlayFactor { get { return playFactor; } }
    public int PetFactor { get { return petFactor; } }
    public int HappinessMax { get { return dogHappinessMax; } }
    public bool IsSad => (dogHappiness == 0);

    private void Awake()
    {
        roomChanger = FindObjectOfType<RoomChanger>();

        isFirstGame = GameSession.IsFirstGame;
    }

    void Start()
    {
        Timer.GameOver += ExportScores;
        RoomChanger.RoomChanged += ToggleCanvas;

        isDebugEnabled = FindObjectOfType<GameSession>().dogDebug;
        toyMessage.gameObject.SetActive(false);

        ResetDogStateTimerMax();
        ResetDogHappiness();
        Sleeping();
    }
    void OnDestroy()
    {
        Timer.GameOver -= ExportScores;
        RoomChanger.RoomChanged -= ToggleCanvas;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Debug 2") && isDebugEnabled) // tab
        {
            dogHappiness = 0;

            Debug.Log("Debug 2 button pressed: Router unplugged");
        }
    }

    // private methods
    private void ExportScores() => FindObjectOfType<ScoreManager>().AddPetScore(petsTotal);

    #region Public Methods
    public void PetDog()
    {
        petsTotal++;
        
        Sleeping();
        
        AddToDogHappiness(petFactor);

        ResetDogStateTimerMax();
    }

    public void ComfortDog()
    {
        petsTotal++;

        glowAnimator.SetBool("DogSad", false);

        Sleeping();

        ResetDogHappiness();

        ResetDogStateTimerMax();
    }

    public void PlayWithDog()
    {
        AddToDogHappiness(playFactor);

        Playing();

        ResetDogStateTimerMax();
    }
    #endregion

    #region State Setting Methods
    void Playing()
    {
        ResetAllStates();

        HideToyRope();

        dogPlaying.SetActive(true);

        toyMessage.gameObject.SetActive(false);

        if (isFirstGame) isFirstGame = false;
    }

    void Relaxing()
    {
        ResetAllStates();

        dogRelaxing.SetActive(true);
    }

    void Sad()
    {
        ResetAllStates();

        glowAnimator.SetBool("DogSad", true);

        UpdateSlider();

        dogSad.SetActive(true);
    }

    void Sleeping()
    {
        ResetAllStates();

        dogSleeping.SetActive(true);
    }

    void Suspicious()
    {
        ResetAllStates();

        dogSuspicious.SetActive(true);
    }
    private void ShowToyRope()
    {
        dogToyRope.SetActive(true);

        //if (isFirstPlay) toyMessage.gameObject.SetActive(true);
        ToggleCanvas();
    }

    private void HideToyRope()
    {
        dogToyRope.SetActive(false);

        //if (isFirstPlay) toyMessage.gameObject.SetActive(false);
        ToggleCanvas();
    }
    #endregion

    #region Utility Methods
    void SetPassiveState()
    {
        int randomNumber = Random.Range(0, 3);

        switch (randomNumber)
        {
            case 0:
                Playing();
                break;
            case 1:
                Relaxing();
                break;
            case 2:
                Suspicious();
                break;
        }
    }
    void ResetAllStates()
    {
        dogPlaying.SetActive(false);
        dogRelaxing.SetActive(false);
        dogSad.SetActive(false);
        dogSleeping.SetActive(false);
        dogSuspicious.SetActive(false);

        ShowToyRope();
    }
    #endregion

    #region Timer Methods
    public void DogTimer()
    {
        if (dogHappiness <= 0)
        {
            UnpluggedRouter?.Invoke();

            Sad();
        }
        else
        {
            dogStateTimerMax--;

            dogHappiness--;

            UpdateSlider();

            if (dogStateTimerMax <= 0)
            {
                if (!roomChanger.ActiveRoomName.Equals("Living Room"))
                {
                    ResetDogStateTimerMax();

                    SetPassiveState();
                }
            }
        }
    }

    void ResetDogStateTimerMax() => dogStateTimerMax = 3 + Random.Range(1, 3);

    void ResetDogHappiness()
    {
        dogHappiness = dogHappinessMax;
        UpdateSlider();
    }

    void AddToDogHappiness(int happinessFactor)
    {
        if (dogHappiness + happinessFactor > dogHappinessMax)
        {
            happinessFactor = dogHappinessMax - dogHappiness;
        }

        dogHappiness += happinessFactor;

        UpdateSlider();
    }

    #endregion

    #region UI Methods
    void UpdateSlider() => dogSlider.value = (float)dogHappiness / (dogHappinessMax - 1);

    void ToggleCanvas() => toyMessage.gameObject.SetActive(roomChanger.ActiveRoomName == "Living Room" && isFirstGame);
    #endregion
}