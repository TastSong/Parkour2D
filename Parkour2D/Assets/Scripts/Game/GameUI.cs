using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Game场景UI显示
public class GameUI : MonoBehaviour
{
    public Text scoreText;
    public BgCtr bgCtr;
    public GroundCtr groundCtr;
    public Button gameOverBtn;
    public GameObject gameOverUI;

    private bool isGameOver = false;

    private void Start() {
        gameOverBtn.onClick.AddListener(() => {          
            GameRestart();   
        });
    }

    private void Update() {
        scoreText.text = GameCtr.manager.coinNum.ToString();

        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if (xboxA > XBOXInput.detectionThreshold && isGameOver) {
            gameOverBtn.onClick.Invoke();
        }
    }

    public void GameOver() {
        bgCtr.isStopBgMove = true;
        groundCtr.isStopGroundMove = true;
        gameOverUI.SetActive(true);
        AnimMan.manager.isPlayerDead = true;
        AudioMan.manager.PlayGameOverAudio();
        isGameOver = true;
    }

    private void GameRestart() {
        gameOverUI.SetActive(false);
        AnimMan.manager.isPlayerDead = false;
        bgCtr.isStopBgMove = false;
        groundCtr.isStopGroundMove = false;
        PlayerCtr.manager.SetPlayerBornPos();
        isGameOver = false;
    }
}
