using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum SkillType {
    Fly = 0,
    Speed
}

public class CircularRingAnim : MonoBehaviour {
    public SkillType skillType;

    private float animTime;
    private Image selfImage;
    private float percentage;

    private void OnEnable() {
        if (skillType == SkillType.Fly) {
            animTime = PlayerCtr.manager.flyTime;
        } else if (skillType == SkillType.Speed){
            animTime = PlayerCtr.manager.speedTime;
        }

        selfImage = GetComponent<Image>();
        percentage = 0;
        selfImage.materialForRendering.SetFloat("_Percentage", percentage);
        DOTween.To(() => percentage, x => percentage = x, 360, animTime);
    }

    private void Update() {
        selfImage.materialForRendering.SetFloat("_Percentage", percentage);
    }
}
