using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCtr : MonoBehaviour {
    public GameObject[] bgGo;
    public bool isStopBgMove = false;
    public float speed = 200f;
    public float startSpeed;

    private float bgStartPosX;
    private float bgStartPosY;
    private float bgStartPosZ;

    private void Start() {
        bgStartPosX = bgGo[bgGo.Length - 1].transform.position.x;
        bgStartPosY = bgGo[bgGo.Length - 1].transform.position.y;
        bgStartPosZ = bgGo[bgGo.Length - 1].transform.position.z;

        startSpeed = speed;
    }

    private void Update() {
        if (!isStopBgMove) {
            for (int i = 0; i < bgGo.Length; i++) {
                bgGo[i].transform.position = new Vector3(bgGo[i].transform.position.x - speed * Time.deltaTime, bgStartPosY, bgStartPosZ);
                if (bgGo[i].transform.position.x < -bgStartPosX) {
                    bgGo[i].transform.position = new Vector3(bgStartPosX, bgStartPosY, bgStartPosZ);
                }
            }
        }
    }
}
