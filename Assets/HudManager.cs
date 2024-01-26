using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance;
    [SerializeField] Canvas canvas;
    [SerializeField] Image weaponPanel;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    public void AddWeaponUI(WeaponBehavior weapon)
    {
        var weaponReferenceUI = Instantiate(weapon.weaponUiPrefab, weaponPanel.transform.position, Quaternion.identity);
        weapon.weaponUiReference = weaponReferenceUI;
        weapon.weaponUiReference.Init(weapon.name, $"{weapon.ammoInChaimber}/{weapon.clipSize} : {weapon.ammoAmount}");
        weapon.weaponUiReference.transform.SetParent(weaponPanel.transform, false);
    }

}
