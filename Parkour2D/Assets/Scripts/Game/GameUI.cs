using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Game场景UI显示
public class GameUI : MonoBehaviour
{
    public Text scoreText;
    public Button jumpBtn;
    public bool isJump = false;
    public Button attackBtn;
    public bool isAttack = false;
    public GameObject gameOverUI;

    private void Start() {
        jumpBtn.onClick .AddListener(() => {
            isJump = true;
        });

        attackBtn.onClick.AddListener(() => {
            isAttack = true;
        });
    }

    private void Update() {
        scoreText.text = GameCtr.manager.score.ToString();
    }

    public void GameOver() {       
        gameOverUI.SetActive(true);      
    }

    public void GameRestart() {
        gameOverUI.SetActive(false);        
    }
}
