using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private List<Image> attachments;
    [SerializeField] private List<GameObject> ammo;

    [SerializeField] private Image shotgun;
    [SerializeField] private Image magazine;
    [SerializeField] private Image scope;
    [SerializeField] private Image rocket;

    [SerializeField] private Sprite empty;

    private int shotgunCount = 0;
    private int magazineCount = 0;
    private int scopeCount = 0;
    private int rocketCount = 0;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DrawGun(scope, scopeCount);
        DrawGun(magazine, magazineCount);
        DrawGun(shotgun, shotgunCount);
        DrawGun(rocket, rocketCount);
    }

    public void UpdateAmmo(int current)
    {
        for (int i = 0; i < Math.Min(current, ammo.Count); i++)
        {
            ammo[i].SetActive(true);
        }

        for (int i = current; i < ammo.Count; i++)
        {
            ammo[i].SetActive(false);
        }
    }

    public void SetIcon(Sprite s, int index)
    {
        attachments[index].sprite = s;
    }

    public void SetEmpty(int index)
    {
        attachments[index].sprite = empty;
    }

    public void AnimateSlot(int index, float time)
    {
        attachments[index].color = GetColor(time);
    }

    private Color GetColor(float time)
    {
        if (time > 3)
            return Color.white;

        time = Mathf.Sin((3 - time) * Mathf.PI * 2) * 0.5f + 0.5f;
        return Color.Lerp(Color.white, Color.red, time);
    }


    public void EquipShotgun(int value)
    {
        shotgunCount += value;
        DrawGun(shotgun, shotgunCount);
    }

    public void EquipMagazine(int value)
    {
        magazineCount += value;
        DrawGun(magazine, magazineCount);
    }
    
    public void EquipScope(int value)
    {
        scopeCount += value;
        DrawGun(scope, scopeCount);
    }

    public void EquipRocket(int value)
    {
        rocketCount += value;
        DrawGun(rocket, rocketCount);
    }

    private void DrawGun(Image image, int value)
    {
        image.enabled = value > 0;
    }
}
