using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Armor")]
public class Armor : Consumables
{
    [SerializeField] private float armor;

    public override void Use()
    {
        Character.instance.AddArmor(armor);
    }
}
