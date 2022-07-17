using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound Volumes")]
public class SoundVolumes : ScriptableObject
{
    [SerializeField] private float sfx = 1;
    [SerializeField] private float soundtrack = 1;

    public float Sfx { get => sfx; set => sfx = value; }
    public float Soundtrack
    {
        get => soundtrack; set
        {
            soundtrack = value;
            global::Soundtrack.instance.SetVolume(value);
        }
    }
}
