using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashHandler : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(DelayForSplash());
    }


    IEnumerator DelayForSplash()
    {
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(1);
    }
}
