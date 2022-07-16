using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GunStats")]
public class GunStats : ScriptableObject
{
    public float bulletSpeed;
    public float damage;
    public float explosionRange;
    public int maxAmmo;
    public int currentAmmo;
    public int bulletCount;
    public float rateOfFire;

    public bool CanShoot()
    {
        return currentAmmo > 0;
    }

    public void Shoot()
    {
        currentAmmo--;
        if (currentAmmo == 0)
        {
            Gun.instance.StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        const float reloadDuration = 2f;
        yield return new WaitForSeconds(reloadDuration);
        currentAmmo = maxAmmo;
    }

}