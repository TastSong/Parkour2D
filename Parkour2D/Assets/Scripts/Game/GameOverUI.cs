using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        var gamepad = Gamepad.current;
        if (gamepad != null) {
            if (gamepad.aButton.wasPressedThisFrame && gameOverAnim.isAnimOver) {
                gameOverAnim.isAnimOver = false;
                restartBtn.onClick.Invoke();
            } else if (gamepad.bButton.wasPressedThisFrame) {
                gameOverAnim.isAnimOver = false;
                exitBtn.onClick.Invoke();
            }
        }      
    }
}
