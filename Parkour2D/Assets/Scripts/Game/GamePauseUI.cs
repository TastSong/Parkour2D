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
        
        float xboxA = Input.GetAxis(XBOXInput.xboxA);
        if (xboxA > XBOXInput.detectionThreshold ) {
            continueBtn.onClick.Invoke();
        }

        float xboxB = Input.GetAxis(XBOXInput.xboxB);
        if (xboxB > XBOXInput.detectionThreshold ) {
            exitBtn.onClick.Invoke();
        }
    }
}
