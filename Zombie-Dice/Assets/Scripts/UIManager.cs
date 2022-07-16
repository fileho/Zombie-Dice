using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private List<Image> attachments;
    [SerializeField] private Slider ammo;


    private void Awake()
    {
        instance = this;
    }

    public void UpdateAmmo(int current, int max)
    {
        ammo.maxValue = max;
        ammo.value = current;
    }

    public void SetIcon(Sprite s, int index)
    {
        attachments[index].sprite = s;
    }
}
