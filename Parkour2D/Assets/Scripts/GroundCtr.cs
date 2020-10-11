using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCtr : MonoBehaviour
{
    public GroundItem[] GroundItems;
    public float speed = 400f;

    private float bgWidth = 1920f;

    private void Start() {
        if (GroundItems.Length > 0) {
            bgWidth = GroundItems[0].gameObject.GetComponent<RectTransform>().rect.width;
        }
    }

    private void Update() {
        for (int i = 0; i < GroundItems.Length; i++) {
            GroundItems[i].gameObject.transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (GroundItems[i].gameObject.transform.localPosition.x < -bgWidth) {
                GroundItems[i].gameObject.transform.localPosition = new Vector3(bgWidth - speed * Time.deltaTime, 0, 0);
                GroundItems[i].ChangePlatform();
            }
        }
    }
}
