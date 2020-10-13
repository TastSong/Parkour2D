using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 存放所有的声音
public class AudioMan : MonoBehaviour
{
    public static AudioMan manager = null;
    public AudioSource playerJumpAudio;
    public AudioSource gameOverAudio;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    public void PlayPlayerJumpAudio() {
        playerJumpAudio.Play();
    }

    public void PlayGameOverAudio() {
        gameOverAudio.Play();
    }
}
