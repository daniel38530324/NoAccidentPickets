using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject menu;
    public GameObject startInitMenu;
    public GameObject settingMenu;
    public GameObject settingBtn;

    public Dropdown playerCountDropdown;

    public void Btn_StartGame()
    {
        menu.SetActive(false);
        startInitMenu.SetActive(true);
    }

    public void Btn_StartInit()
    {
        startInitMenu.SetActive(false);
        gameManager.Initialize(playerCountDropdown.value + 4);
        settingBtn.SetActive(true);
    }

    public void Btn_BackToMenu()
    {
        menu.SetActive(true);
        startInitMenu.SetActive(false);
        settingBtn.SetActive(false);
        settingMenu.SetActive(false);
    }

    public void Btn_Setting()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
    }

    public void Btn_Quit()
    {
        Application.Quit();
    }
}
