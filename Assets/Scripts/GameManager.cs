using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerCount;
    public int currentPlayerId;
    public int[] playerScores;

    //開始遊戲時初始化
    public void Initialize(int _playerCount)
    {
        playerCount = _playerCount;
        currentPlayerId = 0;
        playerScores = new int[_playerCount];
    }

    //掃完卡牌後得分
    public void GetScore(int _score)
    {
        playerScores[currentPlayerId] += _score;
        NextRound();
    }
    
    //輪到下為玩家行動時呼叫
    public void NextRound()
    {
        currentPlayerId++;
        currentPlayerId /= playerCount;
    }
}
