using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Values : MonoBehaviour
{
    public static Action onSetValues;

    private void Awake()
    {
        SetDefaultValues();
    }

    private void OnEnable()
    {
        Upgrade.onUpgrade += SetUpgrade;
    }
    private void OnDisable()
    {
        Upgrade.onUpgrade -= SetUpgrade;
    }

    private void SetDefaultValues()
    {
        PlayerPrefs.SetInt("energy", 500);

        PlayerPrefs.SetFloat("playerMaxHp", 100);
        PlayerPrefs.SetFloat("playerSpeed", 5);
        PlayerPrefs.SetFloat("playerFireRange", 5);
        PlayerPrefs.SetFloat("playerFireForce", 10);
        PlayerPrefs.SetFloat("playerFireRate", 0);
        PlayerPrefs.SetFloat("playerDamage", 1);

        onSetValues?.Invoke();
    }

    public void SetUpgrade(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        onSetValues?.Invoke();
    }
}
