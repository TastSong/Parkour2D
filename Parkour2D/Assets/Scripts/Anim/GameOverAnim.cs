using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameOverAnim : MonoBehaviour
{
    public Image gameOverBg;
    public float gameOverBgTiem = 0.3f;
    public Image scoreImage;
    public Text scoreText;
    public float scoreAnimTime = 0.3f;

    private int startScore = 0;

    private void OnEnable() {
        gameOverBg.fillAmount = 0;
        scoreText.text = startScore.ToString();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => gameOverBg.fillAmount, x => gameOverBg.fillAmount = x, 1f, gameOverBgTiem));
        sequence.Append(scoreImage.transform.DORotate(new Vector3(0, 360, 0), scoreAnimTime, RotateMode.FastBeyond360));
        StartCoroutine(ScoreChangeAnim());
        sequence.SetEase(Ease.InOutCubic);
    }

    private IEnumerator ScoreChangeAnim() {
        yield return new WaitForSecondsRealtime(gameOverBgTiem);
        float scoreAnimTimer = scoreAnimTime;       
        float offsetTime = scoreAnimTime / GameCtr.manager.coinNum;
        int curScore;
        while (true) {
            scoreAnimTimer -= Time.deltaTime;
            curScore = (int)Math.Ceiling((int.Parse(scoreText.text) + offsetTime));
            if (curScore < GameCtr.manager.coinNum && scoreAnimTimer > 0) {
                scoreText.text = curScore.ToString();
            } else {
                scoreText.text = GameCtr.manager.coinNum.ToString();
                break;
            }          
            yield return new WaitForSeconds(offsetTime);
        }
    }
}
