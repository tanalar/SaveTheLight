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
    }

    private void Upgrade()
    {
        hpMultiplier += 0.025f;
        speedMultiplier += 0.0025f;
        sizeMultiplier += 0.0025f;
        spawnRateMultiplier += 0.005f;

        PlayerPrefs.SetFloat("enemyHpMultiplier", hpMultiplier);
        PlayerPrefs.SetFloat("enemySpeedMultiplier", speedMultiplier);
        PlayerPrefs.SetFloat("enemySizeMultiplier", sizeMultiplier);
        PlayerPrefs.SetFloat("enemySpawnRateMultiplier", spawnRateMultiplier);
    }
}
