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
    public Text gameTimeText;
    public Image[] lifeImages;
    public Text scoreText;
    public Button jumpBtn;
    public bool isJump = false;
    public Button attackBtn;
    public bool isAttack = false;
    public GameOverUI gameOverUI;
    public GamePauseUI gamePauseUI;

    private int gameTimer;

    private void OnEnable() {
        InitUI();
    }

    public void InitUI() {
        GameController.manager.isGameOver = false;
        gameTimer = GameController.manager.gameTime;
        GameController.manager.isInGameTime = true;
        GameController.manager.curPlayerLife = GameController.manager.playerLife;
        for (int i = 0; i < GameController.manager.playerLife; i++) {
            lifeImages[i].gameObject.SetActiveFast(GameController.manager.curPlayerLife > i);
        }
        gameTimeText.text = gameTimer.ToString();
        scoreText.text = GameController.manager.score.ToString();
    }

    private void Start() {
        pauseBtn.onClick.AddListener(() => {
            gamePauseUI.gameObject.SetActive(true);
            gamePauseUI.scoreText.text = GameController.manager.score.ToString();
            GameController.manager.IsGamePause(true);                
        });

        jumpBtn.onClick .AddListener(() => {
            isJump = true;
        });

        attackBtn.onClick.AddListener(() => {
            isAttack = true;
        });

        StartCoroutine(GameTime());
    }

    private IEnumerator GameTime() {
        while (true) {
            gameTimeText.text = gameTimer.ToString();
            yield return new WaitForSecondsRealtime(1f);
            if (!GameController.manager.isGameOver) {
                gameTimer -= 1;
            }          
            if (gameTimer <= 0) {
                gameTimeText.text = gameTimer.ToString();
                GameController.manager.isInGameTime = false;
                GameController.manager.CheckGameOver();
                break;
            }
        }
    }

    private void Update() {
        scoreText.text = GameController.manager.score.ToString();
    }

    public void GameOver() {
        for (int i = 0; i < GameController.manager.playerLife; i++) {
            lifeImages[i].gameObject.SetActiveFast(GameController.manager.curPlayerLife > i);
        }
        gameOverUI.gameObject.SetActiveFast(true);
        gameOverUI.gameOverAnim.PlayAnim();
    }

    public void GameRestart() {
        gameOverUI.gameObject.SetActiveFast(false);        
    }

    public void GameContinue() {
        for (int i = 0; i < GameController.manager.playerLife; i++) {
            lifeImages[i].gameObject.SetActiveFast(GameController.manager.curPlayerLife > i);
        }
    }

    public void SetFlySkill(bool isShow) {
        flySkill.gameObject.SetActive(isShow);
    }

    public void SetSpeedSkill(bool isShow) {
        speedSkill.gameObject.SetActive(isShow);
    }
}
