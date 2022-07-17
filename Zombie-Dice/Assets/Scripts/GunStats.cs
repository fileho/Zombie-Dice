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
    public float massModifier;

    [Space]
    [SerializeField] private AudioClip reloadRifleClip;
    [SerializeField] private AudioClip reloadShotgunClip;
    [Space]
    [SerializeField] private AudioClip shotgun;
    [SerializeField] private AudioClip sniper;
    [SerializeField] private AudioClip pistol;
    [SerializeField] private AudioClip rocketLauncher;
    [SerializeField] private AudioClip shotgunCombined;
    [SerializeField] private AudioClip sniperLauncher;

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

        var sound = SelectSoundEffect();
        SoundManager.instance.Play(sound);

        DrawAmmo();
    }

    private IEnumerator Reload()
    {
        const float reloadDuration = 2f;
        yield return new WaitForSeconds(reloadDuration * 0.5f);

        AudioClip clip = bulletCount == 1 ? reloadRifleClip : reloadShotgunClip;
        SoundManager.instance.Play(clip);

        yield return new WaitForSeconds(reloadDuration * 0.5f);
        currentAmmo = maxAmmo;
        DrawAmmo();
    }

    public void DrawAmmo()
    {
        UIManager.instance.UpdateAmmo(currentAmmo);
    }

    private AudioClip SelectSoundEffect()
    {
        const float normalSpeed = 5;

        if (bulletCount > 1)
        {
            if (explosionRange > 0 || bulletCount > normalSpeed)
                return shotgunCombined;
            return shotgun;
        }

        if (explosionRange > 0)
            return bulletSpeed > normalSpeed ? sniperLauncher : rocketLauncher;
        
        if (bulletSpeed > normalSpeed)
            return sniper;

        return pistol;
    }

}
