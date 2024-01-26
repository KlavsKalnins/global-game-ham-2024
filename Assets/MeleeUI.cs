using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeleeUI : MonoBehaviour
{
    [SerializeField] TMP_Text name;
    [SerializeField] Image reloadingImage;

    public void Init(string name = "Fist")
    {
        this.name.text = name;
    }

    public void StartReload(float time)
    {
        StartCoroutine(ReloadCoroutine(time));
    }

    private IEnumerator ReloadCoroutine(float reloadTime)
    {
        reloadingImage.fillAmount = 1f;
        float timer = 0f;
        float startFillAmount = reloadingImage.fillAmount;

        while (timer < reloadTime)
        {
            float progress = timer / reloadTime;

            reloadingImage.fillAmount = Mathf.Lerp(startFillAmount, 0f, progress);

            timer += Time.deltaTime;

            yield return null;
        }

        reloadingImage.fillAmount = 0f;
    }
}
