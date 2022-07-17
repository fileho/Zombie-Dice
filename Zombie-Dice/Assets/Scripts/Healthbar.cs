using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider hpbar;
    [SerializeField] private Slider armorbar;

    public static Healthbar instance;

    private void Awake()
    {
        instance = this;
    }


    public void UpdateHealthbar(float value)
    {
        hpbar.value = value;
    }

    public void UpdateArmorbar(float value)
    {
        armorbar.value = value;
    }
}
