using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RotateAnim : MonoBehaviour
{
    private void OnEnable() {
        Image selfImage = GetComponent<Image>();
        selfImage.transform.DOLocalRotate(new Vector3(0, 360 * 2, 0), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic);
    }
}
