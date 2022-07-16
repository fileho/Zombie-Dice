using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Magazine")]
public class Magazine : Attachment
{
    public int extraBullets;
    [Tooltip(".2 means extra 20 percent")]
    public float rateOfFire;

    public override void Apply(GunStats gun)
    {
        Modify(gun, true);
    }

    public override void Remove(GunStats gun)
    {
        Modify(gun, false);

        if (gun.currentAmmo > gun.maxAmmo)
            gun.currentAmmo = gun.maxAmmo;
    }

    private void Modify(GunStats gun, bool equip)
    {
        int value = equip ? 1 : -1;

        gun.maxAmmo += extraBullets * value;
        gun.rateOfFire += rateOfFire * value;

        gun.DrawAmmo();
    }
}
