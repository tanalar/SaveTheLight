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

    [SerializeField] private GameObject selectionMenu;
    [SerializeField] private List<GameObject> upgradeMenus;
    [SerializeField] private GameObject playerUpgradeMenu;

    public bool pauseGame = false;

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

    public void Confirm()
    {
        upgradesButton.SetActive(true);
        energyCounter.SetActive(true);
        addLightBubbon.SetActive(true);
        joystick.SetActive(true);

        for (int i = 0; i < upgradeMenus.Count; i++)
        {
            upgradeMenus[i].SetActive(false);
        }
        selectionMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        Time.timeScale = 1;
        pauseGame = false;
    }

    public void BackToSelectionMenu()
    {
        for (int i = 0; i < upgradeMenus.Count; i++)
        {
            upgradeMenus[i].SetActive(false);
        }
        selectionMenu.SetActive(true);
    }

    public void PlayerUpgradeMenu()
    {
        selectionMenu.SetActive(false);
        playerUpgradeMenu.SetActive(true);
    }
}
