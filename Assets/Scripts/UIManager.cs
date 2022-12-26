using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject menu;
    
    public GameObject startInitMenu;
    public Dropdown playerCountDropdown;

    public GameObject settingMenu;
    public GameObject settingBtn;

    public GameObject[] playerScorePanel;
    public Text[] scoreTexts;

    public GameObject systemCanvas;
    public Text systemText;
    public GameObject passBtn;
    public GameObject closeAnimBtn;

    public void Initialize(int playerCount)
    {
        if(playerCount == 4){
            playerScorePanel[0].SetActive(false);
            playerScorePanel[1].SetActive(false);
        }else if(playerCount == 5){
            playerScorePanel[0].SetActive(true);
            playerScorePanel[1].SetActive(false);
        }else if(playerCount == 6){
            playerScorePanel[0].SetActive(true);
            playerScorePanel[1].SetActive(true);
        }
        passBtn.SetActive(true);
        closeAnimBtn.SetActive(false);
    }

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
        for (int i = 0; i < 6; i++)
        {
            scoreTexts[i].text = "0";
        }
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

    public void SetScoreText(int playerID, int score)
    {
        scoreTexts[playerID].text = score.ToString();
    }

    public void SetSystemText(bool _open, string _text)
    {
        systemCanvas.SetActive(_open);
        systemText.text = _text;
    }

    public void Btn_Skip()
    {
        gameManager.PassRound();
    }

    public void Btn_CloseAnim()
    {
        gameManager.CloseCardTarget();
    }
}
