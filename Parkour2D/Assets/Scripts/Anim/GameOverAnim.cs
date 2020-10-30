using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameOverAnim : MonoBehaviour
{
    public bool isAnimOver = false;
    public Image gameOverBg;
    public float gameOverBgTiem = 0.3f;
    public Image scoreImage;
    public Text scoreText;
    public float scoreAnimTime = 0.3f;

    private int startScore = 0;

    public void PlayAnim(Util.NoneParamFunction success = null) {
        gameOverBg.fillAmount = 0;
        scoreText.text = startScore.ToString();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => gameOverBg.fillAmount, x => gameOverBg.fillAmount = x, 1f, gameOverBgTiem));
        sequence.Append(scoreImage.transform.DORotate(new Vector3(0, 360, 0), scoreAnimTime, RotateMode.FastBeyond360));
        StartCoroutine(ScoreChangeAnim());
        sequence.SetEase(Ease.InOutCubic);
        sequence.OnComplete(() => {
            isAnimOver = true;
            success?.Invoke();
        });
    }

    private IEnumerator ScoreChangeAnim() {
        yield return new WaitForSecondsRealtime(gameOverBgTiem);
        if (GameController.manager.score >= 0) {
            float scoreAnimTimer = scoreAnimTime;
            float offsetTime = scoreAnimTime / GameController.manager.score;
            int curScore;
            while (true) {
                scoreAnimTimer -= Time.deltaTime;
                curScore = (int)Math.Ceiling((int.Parse(scoreText.text) + offsetTime));
                if (curScore < GameController.manager.score && scoreAnimTimer > 0) {
                    scoreText.text = curScore.ToString();
                } else {
                    scoreText.text = GameController.manager.score.ToString();
                    isAnimOver = true;
                    break;
                }
                yield return new WaitForSeconds(offsetTime);
            }
        } else {
            isAnimOver = true;
        }    
    }
}
