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

    private void OnEnable() {
        if (skillType == SkillType.Fly) {
            animTime = PlayerCtr.manager.flyTime;
        } else if (skillType == SkillType.Speed){
            animTime = PlayerCtr.manager.speedTime;
        }

        selfImage = GetComponent<Image>();
        selfImage.fillAmount = 1;
        DOTween.To(() => selfImage.fillAmount, x => selfImage.fillAmount = x, 0f, animTime);
    }
}
