using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 游戏逻辑控制：背景移动、地面生成、游戏积分、游戏结束
public class GameCtr : MonoBehaviour
{
    public static GameCtr manager = null;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }
}
