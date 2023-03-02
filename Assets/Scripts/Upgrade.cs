using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private string productName;
    [SerializeField] private int upgradeLevels;
    [SerializeField] private Slider slider;
    public static Action<string, float> onUpgrade;
    public static Action onEnergyMinus;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(productName);
    }

    public void productUpgrade()
    {
        int energyCount = PlayerPrefs.GetInt("energy");

        if(slider.value < slider.maxValue && energyCount > 0)
        {
            slider.value = PlayerPrefs.GetFloat(productName);
            slider.value += (slider.maxValue - slider.minValue) / upgradeLevels;
            onUpgrade?.Invoke(productName, slider.value);
            onEnergyMinus?.Invoke();
        }
        else if(slider.value >= slider.maxValue)
        {
            slider.value = slider.maxValue;
        }
    }
}
