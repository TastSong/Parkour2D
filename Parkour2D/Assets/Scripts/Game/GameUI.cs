using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Game场景UI显示
public class GameUI : MonoBehaviour
{
    public Button pauseBtn;
    public Image flySkill;
    public Image speedSkill;
    public Text scoreText;
    public Button jumpBtn;
    public bool isJump = false;
    public Button attackBtn;
    public bool isAttack = false;
    public GameObject gameOverUI;
    public GamePauseUI gamePauseUI;

    private void Start() {
        pauseBtn.onClick.AddListener(() => {
            gamePauseUI.gameObject.SetActive(true);
            GameController.manager.IsGamePause(true);           
        });

        jumpBtn.onClick .AddListener(() => {
            isJump = true;
        });

        attackBtn.onClick.AddListener(() => {
            isAttack = true;
        });
    }

    private void Update() {
        scoreText.text = GameController.manager.score.ToString();
    }

    public void GameOver() {       
        gameOverUI.SetActive(true);      
    }

    public void GameRestart() {
        gameOverUI.SetActive(false);        
    }

    public void SetFlySkill(bool isShow) {
        flySkill.gameObject.SetActive(isShow);
    }

    public void SetSpeedSkill(bool isShow) {
        speedSkill.gameObject.SetActive(isShow);
    }
}
