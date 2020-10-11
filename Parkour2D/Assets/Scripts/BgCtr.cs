﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCtr : MonoBehaviour
{
    public GameObject[] bgGo;
    public float speed = 200f;

    private float bgWidth = 1920f;

    private void Start() {
        if (bgGo.Length > 0) {
            bgWidth = bgGo[0].GetComponent<RectTransform>().rect.width;
        }
    }

    private void Update() {
        for (int i = 0; i < bgGo.Length; i++) {
            bgGo[i].transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (bgGo[i].transform.localPosition.x < -bgWidth) {
                bgGo[i].transform.localPosition = new Vector3(bgWidth - speed * Time.deltaTime, 0, 0);
            }
        }
    }
}
