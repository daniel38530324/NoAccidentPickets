using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uIManager;
    public int playerCount;
    public int currentPlayerId;
    public int[] playerScores;
    public bool isWaitForAnim;
    public bool closeAnim;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N) && !isWaitForAnim){
            GetScore(1);
        }
    }

    //開始遊戲時初始化
    public void Initialize(int _playerCount)
    {
        uIManager.Initialize(_playerCount);
        playerCount = _playerCount;
        currentPlayerId = 0;
        playerScores = new int[_playerCount];
        uIManager.SetSystemText(true, currentPlayerId);
    }

    //掃完卡牌後得分
    public void GetScore(int _score)
    {
        if (isWaitForAnim)
            return;
        playerScores[currentPlayerId] += _score;
        uIManager.SetScoreText(currentPlayerId, playerScores[currentPlayerId]);
        uIManager.SetSystemText(false, 0);
        isWaitForAnim = true;
        StartCoroutine(NextRound());
    }
    
    //輪到下為玩家行動時呼叫
    public IEnumerator NextRound()
    {
        while(!closeAnim){
            uIManager.passBtn.SetActive(false);
            uIManager.closeAnimBtn.SetActive(true);
            yield return null;
            //yield return new WaitForSeconds(20f);//parm=動畫時間，播玩動畫換下一輪
        }
            
        closeAnim = false;
        isWaitForAnim = false;
        uIManager.stars[currentPlayerId].SetActive(false);
        currentPlayerId++;
        currentPlayerId %= playerCount;
        uIManager.stars[currentPlayerId].SetActive(true);
        uIManager.SetSystemText(true, currentPlayerId);
        uIManager.passBtn.SetActive(true);
        uIManager.closeAnimBtn.SetActive(false);
    }

    public void PassRound()
    {
        if (isWaitForAnim)
            return;

        isWaitForAnim = false;
        uIManager.stars[currentPlayerId].SetActive(false);
        currentPlayerId++;
        currentPlayerId %= playerCount;
        uIManager.stars[currentPlayerId].SetActive(true);
        uIManager.SetSystemText(true, currentPlayerId);
    }

    //當沒有相機沒掃到照片時呼叫此方法來輪到下一位玩家
    public void CloseCardTarget()
    {
        closeAnim = true;
    }
}
