using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : ScriptableObject
{
    public Sprite crateIcon;
    public Sprite icon;
    public virtual void Apply(GunStats gun) {;}
    public virtual void Remove(GunStats gun) {;}
}
