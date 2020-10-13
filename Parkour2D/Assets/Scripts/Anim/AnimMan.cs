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
    public bool isPlayerAttack = false;

    private AnimatorStateInfo animInfo;

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
        playerAnim.SetBool("isAttack", isPlayerAttack);

        animInfo = playerAnim.GetCurrentAnimatorStateInfo(0); 
        if ((animInfo.normalizedTime >= 1.0f) && (animInfo.IsName("PlayerAttack")))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
        {
            isPlayerAttack = false;
        }
    }
}
