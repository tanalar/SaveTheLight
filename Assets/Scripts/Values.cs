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
        //Difficult
        PlayerPrefs.SetFloat("enemyHpMultiplier", 1);
        PlayerPrefs.SetFloat("enemySpeedMultiplier", 1);
        PlayerPrefs.SetFloat("enemySizeMultiplier", 1);
        PlayerPrefs.SetFloat("enemySpawnRateMultiplier", 0);

        //Energy
        PlayerPrefs.SetInt("energy", 1000);

        //Player
        PlayerPrefs.SetFloat("playerMaxHp", 100);
        PlayerPrefs.SetFloat("playerSpeed", 5);
        PlayerPrefs.SetFloat("playerFireRange", 5);
        PlayerPrefs.SetFloat("playerFireForce", 10);
        PlayerPrefs.SetFloat("playerFireRate", 0);
        PlayerPrefs.SetFloat("playerDamage", 1);

        //Flamethrower
        PlayerPrefs.SetFloat("flamethrowerFireForce", 5);
        PlayerPrefs.SetFloat("flamethrowerDamage", 0.05f);

        //Laser
        PlayerPrefs.SetFloat("laserFireRate", 0.004f);
        PlayerPrefs.SetFloat("laserFireDuration", 0);
        PlayerPrefs.SetFloat("laserDamage", 0.4f);

        //Aoe
        PlayerPrefs.SetFloat("aoeDamage", 0.035f);
        PlayerPrefs.SetFloat("aoeSlowdownMultiplier", 0);
        PlayerPrefs.SetFloat("aoeRadius", 0.75f);

        onSetValues?.Invoke();
    }

    public void SetUpgrade(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        onSetValues?.Invoke();
    }
}
