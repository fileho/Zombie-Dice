using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Shotgun")]
public class Shotgun : Attachment
{
    [SerializeField] private int extraBullets;

    public override void Apply(GunStats gun)
    {
        Modify(gun, true);
    }

    public override void Remove(GunStats gun)
    {
        Modify(gun, false);
    }


    public void Modify(GunStats gun, bool equip)
    {
        int value = equip ? 1 : -1;

        gun.bulletCount += extraBullets * value;
    }
}
