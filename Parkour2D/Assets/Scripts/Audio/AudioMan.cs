using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 存放所有的声音
public class AudioMan : MonoBehaviour
{
    public static AudioMan manager = null;
    public AudioSource playerJumpAudio;
    public AudioSource gameOverAudio;
    public AudioSource playerAttackAudio;
    public AudioSource coinAudio;
    public AudioSource flyAudio;
    public AudioSource backgroundMusic;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }
    private void Start() {
        backgroundMusic.Play();
    }

    public void IsPlayAudio(bool isPlay) {
        playerJumpAudio.mute = !isPlay;
        gameOverAudio.mute = !isPlay;
        playerAttackAudio.mute = !isPlay;
        coinAudio.mute = !isPlay;
        flyAudio.mute = !isPlay;
    }

    public void PlayPlayerJumpAudio() {
        playerJumpAudio.Play();
    }

    public void PlayGameOverAudio() {
        gameOverAudio.Play();
    }

    public void PlayPlayerAttackAudio() {
        playerAttackAudio.Play();
    }

    public void PlayCoinAudio() {
        coinAudio.Play();
    }

    public void PlayFlyAudio() {
        flyAudio.Play();
    }

    public void IsPlayBackgroundMusic(bool isPlay) {
        backgroundMusic.mute = !isPlay;
    }
}
