using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCtr : MonoBehaviour
{
    public GroundItem[] GroundItems;
    public bool isStopGroundMove = false;
    public float speed = 400f;

    private float itemPosX = 1920f;

    private void Start() {
        if (GroundItems.Length > 1) {
            itemPosX = GroundItems[1].transform.position.x;
        }
    }

    private void Update() {
        if (!isStopGroundMove) {
            for (int i = 0; i < GroundItems.Length; i++) {
                GroundItems[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (GroundItems[i].gameObject.transform.position.x < -itemPosX) {
                    GroundItems[i].gameObject.transform.position = new Vector3(itemPosX, 0, 0);
                    GroundItems[i].ChangePlatform();
                }
            }
        }       
    }
}
