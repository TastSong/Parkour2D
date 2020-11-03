using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Button restartBtn;
    public Button exitBtn;
    public Text bestScore;
    public Image newBestIamge;
    public GameOverAnim gameOverAnim;

    private void OnEnable() {
        bestScore.text = GameController.manager.settingsInfo.bestScore.ToString();
        newBestIamge.gameObject.SetActiveFast(GameController.manager.settingsInfo.bestScore == GameController.manager.score);
    }

    private void Start() {
        restartBtn.onClick.AddListener(() => {
            GameController.manager.GameRestart();
        });

        exitBtn.onClick.AddListener(() => {
            GameController.manager.GameExit();
        });
    }

    private void Update() {
        if (Input.GetButtonDown(XBOXInput.xboxA) && gameOverAnim.isAnimOver) {
            gameOverAnim.isAnimOver = false;
            restartBtn.onClick.Invoke();
        }

        if (Input.GetButtonDown(XBOXInput.xboxB) && gameOverAnim.isAnimOver) {
            gameOverAnim.isAnimOver = false;
            exitBtn.onClick.Invoke();
        }
    }
}
