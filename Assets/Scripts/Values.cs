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
        PlayerPrefs.SetInt("energy", 0);

        //Player
        PlayerPrefs.SetFloat("playerMaxHp", 100);
        PlayerPrefs.SetFloat("playerSpeed", 5);
        PlayerPrefs.SetFloat("playerFireRange", 5);
        PlayerPrefs.SetFloat("playerFireForce", 10);
        PlayerPrefs.SetFloat("playerFireRate", 0);
        PlayerPrefs.SetFloat("playerDamage", 1);

        //FlameThrower
        PlayerPrefs.SetFloat("flameThrowerFireForce", 4f);
        PlayerPrefs.SetFloat("flameThrowerDamage", 0.05f);

        onSetValues?.Invoke();
    }

    public void SetUpgrade(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        onSetValues?.Invoke();
    }
}
