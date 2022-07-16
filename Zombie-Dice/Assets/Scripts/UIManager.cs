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


    private void Awake()
    {
        instance = this;
    }

    public void UpdateAmmo(int current, int max)
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
}
