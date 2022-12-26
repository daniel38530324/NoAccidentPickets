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
        uIManager.SetSystemText(true, $"輪到第 <color=red>{currentPlayerId + 1}</color> 玩家");
    }

    //掃完卡牌後得分
    public void GetScore(int _score)
    {
        if (isWaitForAnim)
            return;
        playerScores[currentPlayerId] += _score;
        uIManager.SetScoreText(currentPlayerId, playerScores[currentPlayerId]);
        StartCoroutine(NextRound());
        uIManager.SetSystemText(true, $"播放動畫中...");
    }
    
    //輪到下為玩家行動時呼叫
    public IEnumerator NextRound()
    {
        uIManager.SetSystemText(false, "");
        isWaitForAnim = true;
        uIManager.passBtn.SetActive(false);
        yield return new WaitForSeconds(20f);//parm=動畫時間，播玩動畫換下一輪
        
        isWaitForAnim = false;
        currentPlayerId++;
        currentPlayerId %= playerCount;
        uIManager.SetSystemText(true, $"輪到第 <color=red>{currentPlayerId + 1}</color> 玩家");
        uIManager.passBtn.SetActive(true);
    }

    public void PassRound()
    {
        if (isWaitForAnim)
            return;

        isWaitForAnim = false;
        currentPlayerId++;
        currentPlayerId %= playerCount;
        uIManager.SetSystemText(true, $"輪到第 <color=red>{currentPlayerId + 1}</color> 玩家");
    }
}
