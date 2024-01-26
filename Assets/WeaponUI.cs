using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] TMP_Text name;
    [SerializeField] Image reloadingImage;

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
            // Calculate the progress as a ratio between the timer and reloadTime
            float progress = timer / reloadTime;

            // Update the fill amount based on the progress
            reloadingImage.fillAmount = Mathf.Lerp(startFillAmount, 0f, progress);

            // Increment the timer
            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the fill amount is set to 0 at the end of the reload process
        reloadingImage.fillAmount = 0f;
    }
}
