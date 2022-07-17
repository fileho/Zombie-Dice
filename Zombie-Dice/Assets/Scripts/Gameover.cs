using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public static Gameover instance;


    private void Awake()
    {
        instance = this;
    }

    public void Activate()
    {
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponent<CanvasGroup>().interactable = true;

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
