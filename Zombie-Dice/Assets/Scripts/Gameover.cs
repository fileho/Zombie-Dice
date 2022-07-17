using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gameover : MonoBehaviour
{
    private CanvasGroup cg;

    public static Gameover instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        transform.Find("Score").Find("Number").GetComponent<TMP_Text>().text = EnemySpawner.instance.Wave().ToString();
    }

    public void Activate()
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        Time.timeScale = 0.1f;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
