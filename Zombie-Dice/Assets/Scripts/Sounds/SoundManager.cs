using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private int poolSize = 10;
    public SoundVolumes volumes;

    public static SoundManager instance;

    List<AudioSource> audioSources = new List<AudioSource>();

    private int index;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var o = new GameObject("audio");
            o.transform.parent = transform;
            o.AddComponent<AudioSource>();
            audioSources.Add(o.GetComponent<AudioSource>());
        }
    }


    public void Play(AudioClip clip, float volume = 1)
    {
        var source = audioSources[index];
        source.clip = clip;
        source.volume = volume * volumes.Sfx;
        source.Play();

        IncrementIndex();
    }

    private void IncrementIndex()
    {
        ++index;
        index %= poolSize;
    }


}
