using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsItem : MonoBehaviour
{
    public GameObject[] coins;

    private void OnEnable() {
        for (int i = 0; i < coins.Length; i++) {
            coins[i].SetActive(true);
        }
    }
}
