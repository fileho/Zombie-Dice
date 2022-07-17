using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    private Slider bar;

    private int count;

    private void Start()
    {
        bar = GetComponentInChildren<Slider>();
        bar.gameObject.SetActive(false);
    }

    public void Show(float value)
    {
        StartCoroutine(ShowBar(value));
    }

    private IEnumerator ShowBar(float value)
    {
        ++count;
        bar.gameObject.SetActive(true);
        bar.value = value;

        yield return new WaitForSeconds(1.5f);

        --count;

        if (count == 0)
        {
            bar.gameObject.SetActive(false);
        }
    }


}
