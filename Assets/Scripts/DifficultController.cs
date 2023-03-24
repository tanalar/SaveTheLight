using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class DifficultController : MonoBehaviour
{
    private float hpMultiplier;
    private float speedMultiplier;
    private float sizeMultiplier;
    private float spawnRateMultiplier;
    private float bonfireFadeMultiplier;

    private void Start()
    {
        SetValues();
    }

    private void OnEnable()
    {
        Enemy.onDeath += Upgrade;
    }
    private void OnDisable()
    {
        Enemy.onDeath -= Upgrade;
    }

    private void SetValues()
    {
        hpMultiplier = PlayerPrefs.GetFloat("enemyHpMultiplier");
        speedMultiplier = PlayerPrefs.GetFloat("enemySpeedMultiplier");
        sizeMultiplier = PlayerPrefs.GetFloat("enemySizeMultiplier");
        spawnRateMultiplier = PlayerPrefs.GetFloat("enemySpawnRateMultiplier");
        bonfireFadeMultiplier = PlayerPrefs.GetFloat("bonfireFadeMultiplier");
    }

    private void Upgrade()
    {
        hpMultiplier += 0.01f;
        speedMultiplier += 0.0005f;
        sizeMultiplier += 0.0005f;
        spawnRateMultiplier += 0.0005f;
        bonfireFadeMultiplier += 0.00002f;

        PlayerPrefs.SetFloat("enemyHpMultiplier", hpMultiplier);
        PlayerPrefs.SetFloat("enemySpeedMultiplier", speedMultiplier);
        PlayerPrefs.SetFloat("enemySizeMultiplier", sizeMultiplier);
        PlayerPrefs.SetFloat("enemySpawnRateMultiplier", spawnRateMultiplier);
        PlayerPrefs.SetFloat("bonfireFadeMultiplier", bonfireFadeMultiplier);
    }
}
