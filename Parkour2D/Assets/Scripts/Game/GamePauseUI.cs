using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
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
        
        if (Input.GetButtonDown(XBOXInput.xboxA)) {
            continueBtn.onClick.Invoke();
        }

        if (Input.GetButtonDown(XBOXInput.xboxB)) {
            exitBtn.onClick.Invoke();
        }
    }
}
