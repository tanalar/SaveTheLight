using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject upgradesButton;
    [SerializeField] private GameObject energyCounter;
    [SerializeField] private GameObject addLightBubbon;
    [SerializeField] private GameObject joystick;

    public bool pauseGame = false;

    public void Confirm()
    {
        upgradesButton.SetActive(true);
        energyCounter.SetActive(true);
        addLightBubbon.SetActive(true);
        joystick.SetActive(true);

        upgradeMenu.SetActive(false);
        Time.timeScale = 1;
        pauseGame = false;
    }
    public void CallUpgradeMenu()
    {
        upgradesButton.SetActive(false);
        energyCounter.SetActive(false);
        addLightBubbon.SetActive(false);
        joystick.SetActive(false);

        upgradeMenu.SetActive(true);
        Time.timeScale = 0;
        pauseGame = true;
    }
}
