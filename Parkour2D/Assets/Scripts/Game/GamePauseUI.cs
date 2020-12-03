using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {
    public Button continueBtn;
    public Button exitBtn;
    public Text scoreText;

    private void Start() {
        continueBtn.onClick.AddListener(() => {
            GameController.manager.IsGamePause(false);
            gameObject.SetActiveFast(false);
        });

        exitBtn.onClick.AddListener(() => {
            GameController.manager.GameExit();
        });


    }

    private void Update() {
        var gamepad = Gamepad.current;
        if (gamepad != null) {
            if (gamepad.aButton.wasPressedThisFrame) {
                continueBtn.onClick.Invoke();
            } else if (gamepad.bButton.wasPressedThisFrame) {
                exitBtn.onClick.Invoke();
            }
        }
    }
}
