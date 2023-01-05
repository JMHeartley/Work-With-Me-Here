using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log(currentSceneIndex + 1);

        SceneManager.LoadScene(currentSceneIndex + 1);
    }


}
