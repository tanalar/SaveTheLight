using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Values : MonoBehaviour
{
    public static Action onUpgrade;

    private void Start()
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

        PlayerPrefs.SetFloat("playerHp", 10);
        PlayerPrefs.SetFloat("playerSpeed", 5);
        PlayerPrefs.SetFloat("playerFireDistance", 5);
        PlayerPrefs.SetFloat("playerFireForce", 30);
        PlayerPrefs.SetFloat("playerFireRate", 0.5f);
        PlayerPrefs.SetFloat("playerDamage", 0.5f);
    }

    public void SetUpgrade(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        onUpgrade?.Invoke();
    }
}
