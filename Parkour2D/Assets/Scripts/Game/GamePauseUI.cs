using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public Button continueBtn;
    public GameOverAnim gameOverAnim;

    private void Start() {
        continueBtn.onClick.AddListener(() => {
            GameController.manager.IsGamePause(false);
            gameObject.SetActive(false);
        });
    }

    private void Update() {
        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if (xboxA > XBOXInput.detectionThreshold && gameOverAnim.isAnimOver) {
            gameOverAnim.isAnimOver = false;
            continueBtn.onClick.Invoke();
        }
    }
}
