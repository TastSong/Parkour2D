using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Game场景UI显示
public class GameUI : MonoBehaviour {
    public Button pauseBtn;
    public Image flySkill;
    public Image speedSkill;
    public Text gameTimeText;
    public Image[] lifeImages;
    public Image xboxImage;
    public Text scoreText;
    public Button jumpBtn;
    public bool isJump = false;
    public Button attackBtn;
    public bool isAttack = false;
    public GameOverUI gameOverUI;
    public GamePauseUI gamePauseUI;

    private float gameTimer;
    private float checkConnectXboxTimer;

    private void OnEnable() {
        checkConnectXboxTimer = 60;
        xboxImage.gameObject.SetActiveFast(false);
        StartCoroutine(CheckConnectXBOX());
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

        jumpBtn.onClick.AddListener(() => {
            isJump = true;
        });

        attackBtn.onClick.AddListener(() => {
            isAttack = true;
        });
    }

    private void Update() {
        scoreText.text = GameController.manager.score.ToString();
        SetGameTime();

        if (Input.GetButtonDown(XBOXInput.xboxY)) {
            pauseBtn.onClick.Invoke();
        }
    }

    private void SetGameTime() {
        if (!GameController.manager.isGameOver) {           
            if (gameTimer > 30) {
                gameTimeText.text = "<color=#3D5E0F>" + ((int)gameTimer).ToString() + "″</color>";
            } else if (gameTimer > 0 && gameTimer <= 30) {
                gameTimeText.text = "<color=#9C0F14>" + ((int)gameTimer).ToString() + "″</color>";
            } else if (gameTimer <= 0){
                gameTimeText.text = "<color=#9C0F14>" + ((int)gameTimer).ToString() + "″</color>";
                GameController.manager.isGameOver = true;
                Debug.Log("+++++++++++++++isGameOver " + GameController.manager.isGameOver);
                GameController.manager.CheckGameOver();
            }

            gameTimer -= Time.deltaTime;
        }
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

    private IEnumerator CheckConnectXBOX() {       
        while (checkConnectXboxTimer > 0) {          
            if (GameController.manager.isConnectXbox) {
                xboxImage.gameObject.SetActiveFast(true);
                break;
            }
            checkConnectXboxTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
