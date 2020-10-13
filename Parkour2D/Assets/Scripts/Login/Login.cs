using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    public Button startBtn;
    public Slider loadSceneSlider;

    private AsyncOperation asyncOp = null;
    private float loadProgress;
    private bool isStart = false;

    private void Start() {
        startBtn.onClick.AddListener(() => {
            asyncOp = SceneManager.LoadSceneAsync("Game");
            asyncOp.allowSceneActivation = false;
            startBtn.interactable = false;
        });
    }

    private void Update() {
        float xboxA = Input.GetAxis("XBOXA");
        if (xboxA > 0.9f && !isStart) {
            startBtn.onClick.Invoke();
            isStart = true;
        }
        if (asyncOp != null) {
            //获取加载进度,此处特别注意:加载场景的progress值最大为0.9
            loadProgress = asyncOp.progress;
        }
        if (loadProgress >= 0.9f) {
            loadProgress = 1;
        }
        loadSceneSlider.value = Mathf.Lerp(loadSceneSlider.value, loadProgress, 1 * Time.deltaTime);//滑动块的value以插值的方式紧跟进度值
        if (loadSceneSlider.value > 0.99f) {
            loadSceneSlider.value = 1;
            asyncOp.allowSceneActivation = true;
        }
    }
}
