using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] TMP_Text weaponName;
    [SerializeField] Image reloadingImage;
    [SerializeField] TMP_Text ammoText;

    public void Init(string name, string ammoText)
    {
        if (weaponName != null)
        {
            this.weaponName.text = name;
        }
        this.ammoText.text = ammoText;
    }

    public void StartReload(float time)
    {
        StartCoroutine(ReloadCoroutine(time));
    }

    public void UpdateAmmoText(string ammoText)
    {
        this.ammoText.text = ammoText;
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
