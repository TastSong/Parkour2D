using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsAnim : MonoBehaviour
{
    public Transform settingsBtnPos;
    public Image bg;
    public GameObject btns;
    public float animTime = 0.4f;

    private Vector3 startPos;
    private float pregross = 0;
    private Vector3[] waypoints;
    private int resolution = 2;

    private void OnEnable() {
        startPos = settingsBtnPos.localPosition;
        bg.transform.localPosition = startPos;
        pregross = 0;
        bg.materialForRendering.SetFloat("_Percentage", pregross);
        btns.SetActiveFast(false);

        PlayAnim();
    }

    private void Update() {
        bg.materialForRendering.SetFloat("_Percentage", pregross);
    }

    private void PlayAnim() {
        waypoints = new Vector3[2];
        waypoints[0] = startPos;
        waypoints[1] = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(bg.transform.DOLocalPath(waypoints, duration: animTime, pathType: PathType.Linear, resolution: resolution).SetOptions(false));
        sequence.Join(DOTween.To(() => pregross, x => pregross = x, 0.5f, animTime));
        sequence.OnComplete(() => {
            btns.SetActiveFast(true);
        });
    }
}
