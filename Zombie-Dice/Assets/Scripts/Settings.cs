using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider soundtrack;
    

    private CanvasGroup cg;

    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            OpenSettings();
    }

    private void OpenSettings()
    {
        if (cg.alpha == 0)
            Open();
        else
            Close();
    }

    private void Open()
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        Time.timeScale = 0f;
        Character.instance.SetInteractable(1);

        sfx.value = SoundManager.instance.volumes.Sfx;
        soundtrack.value = SoundManager.instance.volumes.Soundtrack;
    }

    public void Close()
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        Character.instance.SetInteractable(-1);
        Time.timeScale = 1f;
    }

    public void SfxVolume(float value)
    {
        SoundManager.instance.volumes.Sfx = value;
    }

    public void SoundtrackVolume(float value)
    {
        SoundManager.instance.volumes.Soundtrack = value;
    }

}
