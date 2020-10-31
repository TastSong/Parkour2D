using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController manager = null;
    public SystemSettingsInfo settingsInfo;
    public bool isGameOver = false;
    public bool isPause = false;
    public bool isConnectXbox = false;
    public int score = 0;
    public float gameTime = 60;
    public int playerLife = 3;
    public int curPlayerLife;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }

        // 初始化要早于所有被控制类的初始化，执行顺序 1. 从上到下 每个物体Awake；2. 从上到下每个物体Start
        // 所以其他类的初始化尽量都放在Start
        InitSettingsInfo();
    }

    private void InitSettingsInfo() {
        settingsInfo = SystemSettingsInfo.ParseSystemInfo();
        if (settingsInfo == null) {
            Debug.Log("Load local setting");
            settingsInfo = new SystemSettingsInfo();
            settingsInfo.isPlayAudio = true;
            settingsInfo.isPlayBackgroundMusic = true;
            settingsInfo.bestScore = 0;
            SystemSettingsInfo.SaveSystemInfo(settingsInfo);
        }        
    }

    public void IsGamePause(bool isPause) {
        this.isPause = isPause;
        if (isPause) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    private void GameOver() {
        isGameOver = true;
        // 更新最佳记录
        SaveScore();
        AnimMan.manager.isPlayerDead = true;
        AudioMan.manager.PlayGameOverAudio();
        UIManager.manager.GameOver();
    }

    public void GameStart() {
        isGameOver = false;
        curPlayerLife = playerLife;
    }

    public void GameRestart() {
        isGameOver = false;
        score = 0;
        AnimMan.manager.isPlayerDead = false;
        PlayerCtr.manager.SetPlayerBornPos();
        UIManager.manager.GameRestart();      
    }

    public void GameContinue() {
        AnimMan.manager.isPlayerDead = false;
        PlayerCtr.manager.SetPlayerBornPos();
        UIManager.manager.GameContinue();
    }

    public void CheckGameOver() {
        if (curPlayerLife <= 0 || isGameOver) {
            GameOver();
        } else {
            GameContinue();
        }
    }

    public void GameExit() {
        SaveScore();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SaveScore() {
        SystemSettingsInfo info = settingsInfo;
        if (GameController.manager.score > GameController.manager.settingsInfo.bestScore) {
            info.bestScore = score;
            settingsInfo.bestScore = score;
        } else {
            info.bestScore = settingsInfo.bestScore;
        }
        SystemSettingsInfo.SaveSystemInfo(info);
    }
}
