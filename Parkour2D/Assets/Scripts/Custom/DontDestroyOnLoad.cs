using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加载场景时不删除的物体
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour {

    public GameObject[] DontDestroyObjects;

    private static bool isExist = false;

    void Awake() {

        if (!isExist) {
            for (int i = 0; i < DontDestroyObjects.Length; i++) {
                //如果第一次加载，将这些物体设为DontDestroy
                DontDestroyOnLoad(DontDestroyObjects[i]);
            }

            isExist = true;
        } else {
            for (int i = 0; i < DontDestroyObjects.Length; i++) {
                //如果已经存在，则删除重复的物体
                Destroy(DontDestroyObjects[i]);
            }
        }
    }

}