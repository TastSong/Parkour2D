using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 人物移动 碰撞检测
public class PlayerCtr : MonoBehaviour
{
    public static PlayerCtr manager = null;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }
}
