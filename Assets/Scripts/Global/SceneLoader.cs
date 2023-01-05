using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // public events
    public delegate void SceneAction();
    public static event SceneAction StartScene;
    public static event SceneAction NewScene;

    [Header("Transition")]
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 1f;
    [SerializeField] int startSceneIndex = 1;


    public void QuitGame()
    {
        Application.Quit();
    }
    
    #region Transition Methods
    public void LoadStartTransition()
    {
        StartScene?.Invoke();
        NewScene?.Invoke();

        StartCoroutine(LoadTransition(startSceneIndex));
    }

    public void LoadNextTransition()
    {
        NewScene?.Invoke();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        StartCoroutine(LoadTransition(currentSceneIndex + 1));
    }

    public void LoadNextAsync(Slider slider)
    {
        NewScene?.Invoke();
        
        StartCoroutine(AsyncCoroutine(slider));
    }

    IEnumerator LoadTransition(int sceneIndex)
    {
        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator AsyncCoroutine(Slider slider)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        AsyncOperation async = SceneManager.LoadSceneAsync(currentSceneIndex + 1);

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            //Debug.Log("Loading progress: " + (async.progress * 100) + "%");

            float loadPercentage = Mathf.Clamp01(async.progress / 0.9f);

            slider.value = loadPercentage;

            if (async.progress >= 0.9f)
            {
                transitionAnimator.SetTrigger("Start");

                yield return new WaitForSeconds(transitionTime);

                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    #endregion

    #region Non-Transition Methods
    public void LoadStartScene()
    {
        StartScene?.Invoke();
        NewScene?.Invoke();

        SceneManager.LoadScene(startSceneIndex);
    }

    public void LoadNextScene()
    {
        NewScene?.Invoke();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Debug.Log(currentSceneIndex + 1);

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    #endregion
}