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
        GameController.manager.GameStart();
        gameTimer = GameController.manager.gameTime;
        for (int i = 0; i < GameController.manager.playerLife; i++) {
            lifeImages[i].gameObject.SetActiveFast(GameController.manager.curPlayerLife > i);
        }
        gameTimeText.text = "<color=#3D5E0F>" + gameTimer.ToString() + "″</color>";
        scoreText.text = GameController.manager.score.ToString();
    }

    private void Start() {
        pauseBtn.onClick.AddListener(() => {
            gamePauseUI.gameObject.SetActiveFast(true);
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
            if(gameTimer > 30) {
                gameTimeText.text = "<color=#3D5E0F>" + gameTimer.ToString() + "″</color>";
            } else {
                gameTimeText.text = "<color=#9C0F14>" + gameTimer.ToString() + "″</color>";
            }
            yield return new WaitForSecondsRealtime(1f);
            if (!GameController.manager.isGameOver) {
                gameTimer -= 1;
            }          
            if (gameTimer <= 0) {
                gameTimeText.text = "<color=#9C0F14>" + gameTimer.ToString() + "″</color>";
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
        InitUI();
        gameOverUI.gameObject.SetActiveFast(false);        
    }

    public void GameContinue() {
        for (int i = 0; i < GameController.manager.playerLife; i++) {
            lifeImages[i].gameObject.SetActiveFast(GameController.manager.curPlayerLife > i);
        }
    }

    public void SetFlySkill(bool isShow) {
        flySkill.gameObject.SetActiveFast(isShow);
    }

    public void SetSpeedSkill(bool isShow) {
        speedSkill.gameObject.SetActiveFast(isShow);
    }
}
