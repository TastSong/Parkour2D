using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCtr : MonoBehaviour
{
    public GameObject[] bgGo;
    public bool isStopBgMove = false;
    public float speed = 200f;

    private float bgPosX;

    private void Start() {
        if (bgGo.Length > 1) {
            bgPosX = bgGo[1].transform.position.x;
        }
    }

    private void Update() {
        if (!isStopBgMove) {
            for (int i = 0; i < bgGo.Length; i++) {
                bgGo[i].transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (bgGo[i].transform.position.x < -bgPosX) {
                    bgGo[i].transform.position = new Vector3(bgPosX, 0, 0);
                }
            }
        }      
    }
}
