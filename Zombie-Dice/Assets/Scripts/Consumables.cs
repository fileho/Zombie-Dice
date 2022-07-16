using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumables : ScriptableObject
{
    public Sprite crateIcon;
    public AudioClip pickUpSound;
    public virtual void Use() {; }
}
