using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Button restartBtn;
    public Button exitBtn;
    public GameOverAnim gameOverAnim;

    private void Start() {
        restartBtn.onClick.AddListener(() => {
            GameController.manager.GameRestart();
        });

        exitBtn.onClick.AddListener(() => {
            GameController.manager.GameExit();
        });
    }

    private void Update() {
        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if (xboxA > XBOXInput.detectionThreshold && gameOverAnim.isAnimOver) {
            gameOverAnim.isAnimOver = false;
            restartBtn.onClick.Invoke();
        }
    }
}
