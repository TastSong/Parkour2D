using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 游戏逻辑控制：比赛结束判定
public class GameCtr : MonoBehaviour
{
    public static GameCtr manager = null;
    public GameUI gameUI;
    public SystemSettingsInfo settingsInfo;
    public BgCtr bgCtr;
    public GroundCtr groundCtr;
    public int score = 0;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        InitSettingsInfo();
    }

    private void InitSettingsInfo() {
        Debug.Log("+++++++++++++++ GameCtr 初始化设置");
        settingsInfo = SystemSettingsInfo.ParseSystemInfo();
        if (settingsInfo == null) {
            Debug.Log("Load local setting");
            settingsInfo = new SystemSettingsInfo();
            settingsInfo.isPlayAudio = true;
            settingsInfo.isPlayBackgroundMusic = true;
            SystemSettingsInfo.SaveSystemInfo(settingsInfo);
        }

        AudioMan.manager.IsPlayAudio(settingsInfo.isPlayAudio);
        AudioMan.manager.IsPlayBackgroundMusic(settingsInfo.isPlayBackgroundMusic);
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

    public void SetGameSpeed(float bgSpeed = 1, float groundSpeed = 1) {
        bgCtr.speed = bgCtr.startSpeed * bgSpeed;
        groundCtr.speed = groundCtr.startSpeed * groundSpeed;
    }
}
