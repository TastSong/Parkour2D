using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItem : MonoBehaviour
{
    public GameObject[] enemys;

    private void OnEnable() {
        for (int i = 0; i < enemys.Length; i++) {
            enemys[i].SetActive(true);
        }
    }
}
