using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SystemSettingsUI : MonoBehaviour {
    public Button audioBtn;
    public Sprite[] audioSprites;
    public Button musicBtn;
    public Sprite[] musicSprites;

    private void Start() {
        Debug.Log("+++++++++++++++ SystemSettingsUI 初始化设置UI");
        if (GameCtr.manager.settingsInfo.isPlayAudio) {
            audioBtn.GetComponent<Image>().sprite = audioSprites[0];
        } else {
            audioBtn.GetComponent<Image>().sprite = audioSprites[1];
        }

        if (GameCtr.manager.settingsInfo.isPlayBackgroundMusic) {
            musicBtn.GetComponent<Image>().sprite = musicSprites[0];
        } else {
            musicBtn.GetComponent<Image>().sprite = musicSprites[1];
        }

        audioBtn.onClick.AddListener(() => {
            GameCtr.manager.settingsInfo.isPlayAudio = !GameCtr.manager.settingsInfo.isPlayAudio;
            if (GameCtr.manager.settingsInfo.isPlayAudio) {
                audioBtn.GetComponent<Image>().sprite = audioSprites[0];
            } else {
                audioBtn.GetComponent<Image>().sprite = audioSprites[1];
            }
            AudioMan.manager.IsPlayAudio(GameCtr.manager.settingsInfo.isPlayAudio);
            SaveSettings();
        });

        musicBtn.onClick.AddListener(() => {
            GameCtr.manager.settingsInfo.isPlayBackgroundMusic = !GameCtr.manager.settingsInfo.isPlayBackgroundMusic;
            if (GameCtr.manager.settingsInfo.isPlayBackgroundMusic) {
                musicBtn.GetComponent<Image>().sprite = musicSprites[0];
            } else {
                musicBtn.GetComponent<Image>().sprite = musicSprites[1];
            }
            AudioMan.manager.IsPlayBackgroundMusic(GameCtr.manager.settingsInfo.isPlayBackgroundMusic);
            SaveSettings();
        });
    }

    private SystemSettingsInfo GetCurrentInfo() {
        SystemSettingsInfo info = new SystemSettingsInfo();
        info.isPlayAudio = GameCtr.manager.settingsInfo.isPlayAudio;
        info.isPlayBackgroundMusic = GameCtr.manager.settingsInfo.isPlayBackgroundMusic;      
        return info;
    }

    private void SaveSettings() {
        SystemSettingsInfo info = GetCurrentInfo();       
        SystemSettingsInfo.SaveSystemInfo(info);
    }
}

[Serializable]
public class SystemSettingsInfo : ISerializable {
    public bool isPlayAudio;
    public bool isPlayBackgroundMusic;
    static string fileName = "setting.stf";

    public SystemSettingsInfo() {
    }

    private SystemSettingsInfo(SerializationInfo info, StreamingContext ctxt) {
        isPlayAudio = info.GetBoolean("IsPlayAudio");
        isPlayBackgroundMusic = info.GetBoolean("IsPlayBackgroundMusic");
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
        info.AddValue("IsPlayAudio", isPlayAudio);
        info.AddValue("IsPlayBackgroundMusic", isPlayBackgroundMusic);
    }

    public static void SaveSystemInfo(SystemSettingsInfo ss) {
        Stream steam = File.Open(Path.Combine(Application.persistentDataPath, fileName), FileMode.Create);
        Debug.Log("设置文件的路径 ： " + Path.Combine(Application.persistentDataPath, fileName));
        var bf = new BinaryFormatter();
        bf.Serialize(steam, ss);
        steam.Close();
    }

    public static void DelFile() {
        File.Delete(Path.Combine(Application.persistentDataPath, fileName));
    }

    public static SystemSettingsInfo ParseSystemInfo() {
        Stream stream = File.Open(Path.Combine(Application.persistentDataPath, fileName), FileMode.OpenOrCreate);
        BinaryFormatter bf = new BinaryFormatter();
        SystemSettingsInfo mp = null;
        try {
            mp = (SystemSettingsInfo)bf.Deserialize(stream);
        } catch {
            // ignore exception
        }

        stream.Close();
        return mp;
    }

    public SystemSettingsInfo Clone() {
        SystemSettingsInfo info = new SystemSettingsInfo();
        info.isPlayAudio = isPlayAudio;
        info.isPlayBackgroundMusic = isPlayBackgroundMusic;
        return info;
    }

    public bool EqualTo(SystemSettingsInfo info) {
        return info.isPlayAudio == isPlayAudio
               && info.isPlayBackgroundMusic == isPlayBackgroundMusic;
    }
}