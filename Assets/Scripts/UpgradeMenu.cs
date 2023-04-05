using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject upgradesButton;
    [SerializeField] private GameObject energyCounter;
    [SerializeField] private GameObject addLightButton;
    [SerializeField] private GameObject joystick;

    [SerializeField] private GameObject selectionMenu;
    [SerializeField] private List<GameObject> upgradeMenus;
    [SerializeField] private GameObject playerUpgradeMenu;
    [SerializeField] private GameObject flamethrowerUpgradeMenu;
    [SerializeField] private GameObject laserUpgradeMenu;
    [SerializeField] private GameObject aoeUpgradeMenu;
    [SerializeField] private GameObject minigunUpgradeMenu;
    [SerializeField] private GameObject sniperUpgradeMenu;

    public bool pauseGame = false;

    public void CallUpgradeMenu()
    {
        upgradesButton.SetActive(false);
        energyCounter.SetActive(false);
        addLightButton.SetActive(false);
        joystick.SetActive(false);

        upgradeMenu.SetActive(true);
        Time.timeScale = 0;
        pauseGame = true;
    }

    public void Confirm()
    {
        upgradesButton.SetActive(true);
        energyCounter.SetActive(true);
        addLightButton.SetActive(true);
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

    public void FlamethrowerUpgradeMenu()
    {
        selectionMenu.SetActive(false);
        flamethrowerUpgradeMenu.SetActive(true);
    }

    public void LaserUpgradeMenu()
    {
        selectionMenu.SetActive(false);
        laserUpgradeMenu.SetActive(true);
    }

    public void AoeUpgradeMenu()
    {
        selectionMenu.SetActive(false);
        aoeUpgradeMenu.SetActive(true);
    }

    public void MinigunUpgradeMenu()
    {
        selectionMenu.SetActive(false);
        minigunUpgradeMenu.SetActive(true);
    }

    public void SniperUpgradeMenu()
    {
        selectionMenu.SetActive(false);
        sniperUpgradeMenu.SetActive(true);
    }
}
