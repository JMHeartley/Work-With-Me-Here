using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCycle : MonoBehaviour
{
    [SerializeField] float cyclePause = 0.4f;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Cycle(GetComponent<Slider>()));
    }

    IEnumerator Cycle(Slider slider)
    {
        while (true)
        {
            if (slider.value - 1f / 10f < 0) slider.value = 1f;
            else slider.value -= 1f / 10f;

            yield return new WaitForSeconds(cyclePause); 
        }
    }
}
