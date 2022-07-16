using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Scope")]
public class Scope : Attachment
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

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

        gun.damage += damage * value;
        gun.bulletSpeed += speed * value;
    }
}
