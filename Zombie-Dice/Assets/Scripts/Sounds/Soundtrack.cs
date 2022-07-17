using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundtrack : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private AudioSource audioSource;

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
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = SoundManager.instance.volumes.Soundtrack;
        audioSource.Play();
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
