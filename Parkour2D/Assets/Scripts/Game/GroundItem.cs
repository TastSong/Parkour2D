using UnityEngine;
using System;

public class GroundItem : MonoBehaviour
{
    public GameObject[] leftPlatforms;
    public GameObject[] rightPlatforms;

    private int leftPlatformNum;
    private int rightPlatformNum;
    private System.Random random = new System.Random();

    private void Start() {
        leftPlatformNum = leftPlatforms.Length;
        rightPlatformNum = rightPlatforms.Length;
    }

    public void ChangePlatform() {
        int leftNum = random.Next(0, leftPlatformNum);
        int rightNum = random.Next(0, rightPlatformNum);

        for (int i = 0; i < leftPlatformNum; i++) {
            leftPlatforms[i].SetActive(false);
        }

        for (int i = 0; i < rightPlatformNum; i++) {
            rightPlatforms[i].SetActive(false);
        }

        for (int i = 0; i < leftPlatformNum; i++) {
            leftPlatforms[i].SetActive(i == leftNum);
        }

        for (int i = 0; i < rightPlatformNum; i++) {
            rightPlatforms[i].SetActive(i == rightNum);
        }
    }
}
