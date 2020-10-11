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

    private void Start() {
        gameOverBtn.onClick.AddListener(() => {
            gameOverUI.SetActive(false);
            AnimMan.manager.isPlayerDead = false;
        });
    }

    private void Update() {
        scoreText.text = GameCtr.manager.coinNum.ToString();
    }

    public void GameOver() {
        bgCtr.isStopBgMove = true;
        groundCtr.isStopGroundMove = true;
        gameOverUI.SetActive(true);
        AnimMan.manager.isPlayerDead = true;
        AudioMan.manager.PlayGameOverAudio();
    }
}
