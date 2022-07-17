using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundtrack : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;

    private AudioSource audioSource;
    private int songIndex;

    public static Soundtrack instance;

    private void Awake()
    {
        if (FindObjectsOfType<Soundtrack>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        SetVolume(SoundManager.instance.volumes.Soundtrack);
        PlaySong();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
            PlaySong();
    }

    private void PlaySong()
    {
        audioSource.clip = clips[songIndex];
        audioSource.Play();

        IncrementSongIndex();
    }

    private void IncrementSongIndex()
    {
        ++songIndex;
        songIndex %= clips.Count;
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value*0.7f;
    }
}
