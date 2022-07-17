using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : ScriptableObject
{
    public Sprite crateIcon;
    public Sprite icon;
    public AudioClip pickUpSound;

    public float duration = 15f;

    public void UpdateDuration()
    {
        if (duration > 0)
            duration -= Time.deltaTime;
    }

    public bool IsExpired()
    {
        return duration <= 0;
    }

    public virtual void Apply(GunStats gun) {;}
    public virtual void Remove(GunStats gun) {;}
}
