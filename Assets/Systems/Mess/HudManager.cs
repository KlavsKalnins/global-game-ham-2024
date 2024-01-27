using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance;
    [SerializeField] Canvas canvas;
    [SerializeField] Image weaponPanel;
    [SerializeField] CanvasGroup weaponCanvasGroup;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        weaponCanvasGroup.alpha = 0f;
        LeanTween.delayedCall(3, () =>
        {
            LeanTween.alphaCanvas(weaponCanvasGroup, 1f, 3).setEase(LeanTweenType.easeInOutSine);
        });


        LeanTween.alphaCanvas(weaponCanvasGroup, 1f, 3).setEase(LeanTweenType.easeInOutSine);
    }

    public void AddWeaponUI(WeaponBehavior weapon)
    {
        var weaponReferenceUI = Instantiate(weapon.weaponUiPrefab, weaponPanel.transform.position, Quaternion.identity);
        weapon.weaponUiReference = weaponReferenceUI;
        weapon.weaponUiReference.Init(weapon.weaponName, $"{weapon.ammoInChaimber}/{weapon.clipSize} : {weapon.ammoAmount}");
        weapon.weaponUiReference.transform.SetParent(weaponPanel.transform, false);
    }

}
