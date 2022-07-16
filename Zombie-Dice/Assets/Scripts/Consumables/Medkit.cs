using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Medkit")]
public class Medkit : Consumables
{
    [SerializeField] private float hp;

    public override void Use()
    {
        Character.instance.RestoreHP(hp);
    }
}
