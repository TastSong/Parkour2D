using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController manager = null;
    public SystemSettingsInfo settingsInfo;
    public bool isPause = false;
    public int score = 0;

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
            SystemSettingsInfo.SaveSystemInfo(settingsInfo);
        }        
    }

    public void IsGamePause(bool isPause) {
        this.isPause = isPause;
        AnimMan.manager.isPlayerDead = false;
        PlayerCtr.manager.IsPlayerPause();
        UIManager.manager.IsGamePause();
    }

    public void GameOver() {
        AnimMan.manager.isPlayerDead = true;
        AudioMan.manager.PlayGameOverAudio();
        UIManager.manager.GameOver();
    }

    public void GameRestart() {
        AnimMan.manager.isPlayerDead = false;
        PlayerCtr.manager.SetPlayerBornPos();
        UIManager.manager.GameRestart();
        score = 0;
    }
}
