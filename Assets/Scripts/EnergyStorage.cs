using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyStorage : MonoBehaviour
{
    [SerializeField] private Text textCounter;
    [SerializeField] private Text upgradeMenuTextCounter;
    [SerializeField] private Button addLightButton;
    [SerializeField] private Button upgradesButton;
    private float a;
    private float delay = 0.01f;
    private int energyCounter;
    private bool fade = true;

    public static Action onAddLight;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private void OnEnable()
    {
        Energy.onDestroy += EnergyPlus;
        Values.onSetValues += SetEnergyCount;
    }
    private void OnDisable()
    {
        Energy.onDestroy -= EnergyPlus;
        Values.onSetValues -= SetEnergyCount;
    }

    private void EnergyPlus(int value)
    {
        energyCounter += value;
        SetPlayerPrefs();
        textCounter.text = energyCounter.ToString();
        upgradeMenuTextCounter.text = energyCounter.ToString();
    }

    public void AddLight()
    {
        if (energyCounter > 0)
        {
            energyCounter--;
            SetPlayerPrefs();
            onAddLight?.Invoke();
            textCounter.text = energyCounter.ToString();
            upgradeMenuTextCounter.text = energyCounter.ToString();
        }
    }

    private void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("energy", energyCounter);
    }

    private void SetEnergyCount()
    {
        energyCounter = PlayerPrefs.GetInt("energy");
        textCounter.text = energyCounter.ToString();
        upgradeMenuTextCounter.text = energyCounter.ToString();
    }



    //Transparency Animation

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            fade = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fade = true;
        }
    }

    private void Transparency()
    {
        if (fade && a > 0)
        {
            a -= 0.04f;
            if (a < 0)
            {
                a = 0;
                addLightButton.interactable = false;
                upgradesButton.interactable = false;
            }
        }
        if (!fade && a < 1)
        {
            addLightButton.interactable = true;
            upgradesButton.interactable = true;
            a += 0.04f;
            if (a > 1)
            {
                a = 1;
            }
        }
        textCounter.color = new Vector4(1, 1, 1, a);
        addLightButton.image.color = new Vector4(1, 1, 1, a);
        upgradesButton.image.color = new Vector4(1, 1, 1, a);
    }
    private IEnumerator Delay()
    {
        Transparency();
        yield return new WaitForSeconds(delay);
        StartCoroutine(Delay());
    }
}
