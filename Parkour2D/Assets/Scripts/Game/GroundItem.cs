using UnityEngine;
using System;

public class GroundItem : MonoBehaviour
{
    public GameObject[] leftPlatforms;
    public GameObject[] midPlatforms;
    public GameObject[] rightPlatforms;

    private int leftPlatformNum;
    private int midPlatformNum;
    private int rightPlatformNum;
    private System.Random random = new System.Random();

    private void Start() {
        leftPlatformNum = leftPlatforms.Length;
        midPlatformNum = midPlatforms.Length;
        rightPlatformNum = rightPlatforms.Length;
    }

    public void ChangePlatform() {
        int leftNum = random.Next(0, leftPlatformNum);
        int midNum = random.Next(0, midPlatformNum);
        int rightNum = random.Next(0, rightPlatformNum);

        if (leftPlatformNum > 0) {
            for (int i = 0; i < leftPlatformNum; i++) {
                leftPlatforms[i].SetActiveFast(i == leftNum);
            }
        }

        if (midPlatformNum > 0) {
            for (int i = 0; i < midPlatformNum; i++) {
                midPlatforms[i].SetActiveFast(i == midNum);
            }
        }

        if (rightPlatformNum > 0) {
            for (int i = 0; i < rightPlatformNum; i++) {
                rightPlatforms[i].SetActiveFast(i == rightNum);
            }
        }      
    }
}
