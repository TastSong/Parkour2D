using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverAnim : MonoBehaviour
{
    public Image gameOverBg;
    private void OnEnable() {
        gameOverBg.fillAmount = 0;
        DOTween.To(() => gameOverBg.fillAmount, x => gameOverBg.fillAmount = x, 1f, 0.3f).SetEase(Ease.InOutCubic);
    }
}
