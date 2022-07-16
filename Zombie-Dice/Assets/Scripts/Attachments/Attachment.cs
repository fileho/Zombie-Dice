using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : ScriptableObject
{
    public virtual void Apply(GunStats gun) {;}
    public virtual void Remove(GunStats gun) {;}
}
