using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Rocket Launcher")]
public class RocketLauncher : Attachment
{
    [SerializeField] private float explosionRange;

    public override void Apply(GunStats gun)
    {
        Modify(gun, true);
    }

    public override void Remove(GunStats gun)
    {
        Modify(gun, false);
    }

    private void Modify(GunStats gun, bool equip)
    {
        int value = equip ? 1 : -1;

        gun.explosionRange += explosionRange * value;
    }
}
