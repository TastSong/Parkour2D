using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 存放所有的动画
public class AnimMan : MonoBehaviour
{
    public static AnimMan manager = null;
    public Animator playerAnim;
    public bool isPlayerJump = false;
    public bool isPlayerDead = false;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    private void Update() {
        playerAnim.SetBool("isJump", isPlayerJump);
        playerAnim.SetBool("isDead", isPlayerDead);
    }
}
