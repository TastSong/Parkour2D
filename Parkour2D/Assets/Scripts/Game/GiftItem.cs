using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftItem : MonoBehaviour
{
    public GameObject[] gifts;

    private void OnEnable() {
        for (int i = 0; i < gifts.Length; i++) {
            gifts[i].SetActive(true);
        }
    }
}
