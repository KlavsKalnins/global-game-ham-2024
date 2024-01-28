using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthPointUI : MonoBehaviour
{
    [SerializeField] Image healthPointImage;

    [SerializeField] LeanTweenType leanTypeEnabled = LeanTweenType.easeInOutSine;
    [SerializeField] LeanTweenType leanTypeDisabled = LeanTweenType.easeInOutSine;
    [SerializeField] float leanTime = 0.3f;

    public void UpdateVisual(bool state)
    {
        // healthPointImage.enabled = state;

        if (state)
        {
            LeanTween.scale(healthPointImage.rectTransform, new Vector3(1f, 1f, 1f), leanTime)
                .setEase(leanTypeEnabled)
                .setOnComplete(() =>
                {
                });
        }
        else
        {
            LeanTween.scale(healthPointImage.rectTransform, Vector3.zero, leanTime)
                .setEase(leanTypeDisabled)
                .setOnComplete(() =>
                {
                });
        }
    }
}
