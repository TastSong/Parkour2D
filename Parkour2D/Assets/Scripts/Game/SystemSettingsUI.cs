using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SystemSettingsUI : MonoBehaviour {
    public Button backBtn;
    public Button audioBtn;
    public Sprite[] audioSprites;
    public Button musicBtn;
    public Sprite[] musicSprites;

    private void Start() {
        if (backBtn != null) {
            backBtn.onClick.AddListener(() => {
                gameObject.SetActive(false);
            });
        }

        if (GameController.manager.settingsInfo.isPlayAudio) {
            audioBtn.GetComponent<Image>().sprite = audioSprites[0];
        } else {
            audioBtn.GetComponent<Image>().sprite = audioSprites[1];
        }

        if (GameController.manager.settingsInfo.isPlayBackgroundMusic) {
            musicBtn.GetComponent<Image>().sprite = musicSprites[0];
        } else {
            musicBtn.GetComponent<Image>().sprite = musicSprites[1];
        }

        audioBtn.onClick.AddListener(() => {
            GameController.manager.settingsInfo.isPlayAudio = !GameController.manager.settingsInfo.isPlayAudio;
            if (GameController.manager.settingsInfo.isPlayAudio) {
                audioBtn.GetComponent<Image>().sprite = audioSprites[0];
            } else {
                audioBtn.GetComponent<Image>().sprite = audioSprites[1];
            }
            AudioMan.manager.IsPlayAudio(GameController.manager.settingsInfo.isPlayAudio);
            SaveSettings();
        });

        musicBtn.onClick.AddListener(() => {
            GameController.manager.settingsInfo.isPlayBackgroundMusic = !GameController.manager.settingsInfo.isPlayBackgroundMusic;
            if (GameController.manager.settingsInfo.isPlayBackgroundMusic) {
                musicBtn.GetComponent<Image>().sprite = musicSprites[0];
            } else {
                musicBtn.GetComponent<Image>().sprite = musicSprites[1];
            }
            AudioMan.manager.IsPlayBackgroundMusic(GameController.manager.settingsInfo.isPlayBackgroundMusic);
            SaveSettings();
        });
    }

    private SystemSettingsInfo GetCurrentInfo() {
        SystemSettingsInfo info = new SystemSettingsInfo();
        info.isPlayAudio = GameController.manager.settingsInfo.isPlayAudio;
        info.isPlayBackgroundMusic = GameController.manager.settingsInfo.isPlayBackgroundMusic;      
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