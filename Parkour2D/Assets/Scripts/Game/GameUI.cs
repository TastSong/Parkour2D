using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Game场景UI显示
public class GameUI : MonoBehaviour
{
    public Text scoreText;
    public Button gameOverBtn;
    public Button jumpBtn;
    public bool isJump = false;
    public Button attackBtn;
    public bool isAttack = false;
    public GameObject gameOverUI;

    private bool isGameOver = false;

    private void Start() {
        jumpBtn.onClick .AddListener(() => {
            isJump = true;
        });

        attackBtn.onClick.AddListener(() => {
            isAttack = true;
        });

        gameOverBtn.onClick.AddListener(() => {
            GameCtr.manager.GameRestart();  
        });
    }

    private void Update() {
        scoreText.text = GameCtr.manager.score.ToString();

        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if (xboxA > XBOXInput.detectionThreshold && isGameOver) {
            gameOverBtn.onClick.Invoke();
        }
    }

    public void GameOver() {       
        gameOverUI.SetActive(true);
        
        isGameOver = true;
    }

    public void GameRestart() {
        gameOverUI.SetActive(false);        
        isGameOver = false;
    }
}
