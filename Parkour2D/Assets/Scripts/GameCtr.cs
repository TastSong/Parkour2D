using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 游戏逻辑控制：比赛结束判定
public class GameCtr : MonoBehaviour
{
    public static GameCtr manager = null;
    public GameUI gameUI;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    public void GameOver() {
        gameUI.GameOver();
    }
}
