﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCtr : MonoBehaviour
{
    public GroundItem[] GroundItems;
    public bool isStopGroundMove = false;
    public float speed = 400f;

    private float itemPosX = 1920f;
    private float itemPosY = 1920f;
    private float itemPosZ = 1920f;

    private void Start() {
        if (GroundItems.Length > 1) {
            itemPosX = GroundItems[1].transform.position.x;
            itemPosY = GroundItems[1].transform.position.y;
            itemPosZ = GroundItems[1].transform.position.z;
        }
    }

    private void Update() {
        if (!isStopGroundMove) {
            for (int i = 0; i < GroundItems.Length; i++) {
                GroundItems[i].gameObject.transform.position = new Vector3(GroundItems[i].gameObject.transform.position.x - speed * Time.deltaTime, itemPosY, itemPosZ);
                if (GroundItems[i].gameObject.transform.position.x < -itemPosX) {
                    GroundItems[i].gameObject.transform.position = new Vector3(itemPosX, itemPosY, itemPosZ);
                    GroundItems[i].ChangePlatform();
                }
            }
        }       
    }
}
