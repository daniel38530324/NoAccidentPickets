using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject menu;
    
    public GameObject startInitMenu;
    int charSelectedCount = 0;
    public Text charSelectedText;
    public bool[] selectedChar;
    public GameObject[] charBtns;
    public GameObject[] char_circle_Btns;
    public GameObject systemWarnText;

    public GameObject settingMenu;
    public GameObject settingBtn;

    //public GameObject[] playerScorePanel;
    public Text[] scoreTexts;
    public Sprite[] allCharImages;
    public Image[] playerImages;
    public GameObject[] stars;

    public GameObject[] systemCanvas;
    public Image currentPlayerImage;
    public GameObject passBtn;
    public GameObject closeAnimBtn;

    public void Initialize(int playerCount)
    {
        /*if(playerCount == 4){
            playerScorePanel[0].SetActive(false);
            playerScorePanel[1].SetActive(false);
        }else if(playerCount == 5){
            playerScorePanel[0].SetActive(true);
            playerScorePanel[1].SetActive(false);
        }else if(playerCount == 6){
            playerScorePanel[0].SetActive(true);
            playerScorePanel[1].SetActive(true);
        }*/
        passBtn.SetActive(true);
        closeAnimBtn.SetActive(false);
        stars[0].SetActive(true);
        for (int i = 1; i < 3; i++)
        {
            stars[i].SetActive(false);
        }
    }

    public void Btn_StartGame()
    {
        menu.SetActive(false);
        startInitMenu.SetActive(true);
    }

    public void Btn_SelectChar(int charId)
    {
        bool isSelected = selectedChar[charId];
        if(!isSelected){
            if(charSelectedCount == 4){
                systemWarnText.SetActive(true);
                return;
            } 
            
            selectedChar[charId] = !isSelected;
            charBtns[charId].SetActive(false);
            char_circle_Btns[charId].SetActive(true);
            playerImages[charSelectedCount].sprite = allCharImages[charId];
            charSelectedCount++;
        }else{
            selectedChar[charId] = !isSelected;
            charBtns[charId].SetActive(true);
            char_circle_Btns[charId].SetActive(false);
            charSelectedCount--;
        }
        charSelectedText.text = charSelectedCount.ToString();
    }

    public void Btn_StartInit()
    {
        if(charSelectedCount != 4){
                systemWarnText.SetActive(true);
                return;
        }
        startInitMenu.SetActive(false);
        gameManager.Initialize(4);
        settingBtn.SetActive(true);
        for (int i = 0; i < 4; i++)
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
        BackToMenuInitialize();
    }

    void BackToMenuInitialize()
    {
        charSelectedCount = 0;
        charSelectedText.text = "0";
        systemWarnText.SetActive(false);
        for (int i = 0; i < selectedChar.Length; i++)
        {
            selectedChar[i] = false;
            charBtns[i].SetActive(true);
            char_circle_Btns[i].SetActive(false);
        }
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

    public void SetSystemText(bool _open, int _playerIndex)
    {
        systemCanvas[0].SetActive(_open);
        systemCanvas[1].SetActive(!_open);
        currentPlayerImage.sprite = playerImages[_playerIndex].sprite;
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
