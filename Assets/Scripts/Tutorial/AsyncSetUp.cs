using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsyncSetUp : MonoBehaviour
{
    // displayed in editor
    [Header("Set Up")]
    [SerializeField] GameObject loadingButton;
    [SerializeField] Slider progressSlider;

    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Start()
    {
        progressSlider.gameObject.SetActive(false);

        progressSlider.value = 0;
    }

    public void StartLoading()
    {
        loadingButton.SetActive(false);

        progressSlider.gameObject.SetActive(true);

        //progressSlider.transform.position = loadingButton.transform.position;

        sceneLoader.LoadNextAsync(progressSlider);
    }
}