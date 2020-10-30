using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public Button continueBtn;
    public Text scoreText;

    private void Start() {
        continueBtn.onClick.AddListener(() => {
            GameController.manager.IsGamePause(false);
            gameObject.SetActive(false);
        });
    }

    private void Update() {
        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if (xboxA > XBOXInput.detectionThreshold) {
            continueBtn.onClick.Invoke();
        }
    }
}
