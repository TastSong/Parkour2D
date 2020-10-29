using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamController : MonoBehaviour
{
    public static GamController manager = null;
    public SystemSettingsInfo settingsInfo;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }

        // 初始化要早于所有被控制类的初始化，执行顺序 1. 从上到下 每个物体Awake；2. 从上到下每个物体Start
        // 所以其他类的初始化尽量都放在Start
        InitSettingsInfo();
    }

    private void InitSettingsInfo() {
        settingsInfo = SystemSettingsInfo.ParseSystemInfo();
        if (settingsInfo == null) {
            Debug.Log("Load local setting");
            settingsInfo = new SystemSettingsInfo();
            settingsInfo.isPlayAudio = true;
            settingsInfo.isPlayBackgroundMusic = true;
            SystemSettingsInfo.SaveSystemInfo(settingsInfo);
        }        
    }
}
