using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 游戏逻辑控制：比赛结束判定
public class UIManager : MonoBehaviour
{
    public static UIManager manager = null;
    public GameUI gameUI;
    public BgCtr bgCtr;
    public GroundCtr groundCtr;


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
        gameUI.GameOver();
    }

    public void GameRestart() {
        bgCtr.isStopBgMove = false;
        groundCtr.isStopGroundMove = false;
        gameUI.GameRestart();
    }

    public void SetGameSpeed(float bgSpeed = 1, float groundSpeed = 1) {
        bgCtr.speed = bgCtr.startSpeed * bgSpeed;
        groundCtr.speed = groundCtr.startSpeed * groundSpeed;
    }

    public void IsGamePause() {
        bgCtr.isStopBgMove = GameController.manager.isPause;
        groundCtr.isStopGroundMove = GameController.manager.isPause;
    }
}
