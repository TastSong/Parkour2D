using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 游戏逻辑控制：比赛结束判定
public class GameCtr : MonoBehaviour
{
    public static GameCtr manager = null;
    public GameUI gameUI;
    public BgCtr bgCtr;
    public GroundCtr groundCtr;
    public int coinNum = 0;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    public void GameOver() {
        bgCtr.isStopBgMove = true;
        groundCtr.isStopGroundMove = true;
        AnimMan.manager.isPlayerDead = true;
        AudioMan.manager.PlayGameOverAudio();
        gameUI.GameOver();
    }

    public void GameRestart() {     
        bgCtr.isStopBgMove = false;
        groundCtr.isStopGroundMove = false;
        AnimMan.manager.isPlayerDead = false;
        PlayerCtr.manager.SetPlayerBornPos();
        gameUI.GameRestart();
    }
}
