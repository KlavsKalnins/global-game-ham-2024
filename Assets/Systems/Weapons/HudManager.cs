using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance;

    public static Action OnGameStart;

    [SerializeField] Canvas canvas;
    [SerializeField] Image weaponPanel;
    [SerializeField] CanvasGroup weaponCanvasGroup;

    [SerializeField] CanvasGroup mainMenuCanvasGroup;

    [SerializeField] GameObject playerObject;

    private void OnEnable()
    {
        PlayerHive.OnPlayerDeath += OnReloadGame;
    }
    private void OnDisable()
    {
        PlayerHive.OnPlayerDeath -= OnReloadGame;

    }

    private void Awake()
    {
        Instance = this;
        AudioManager.Instance.PlayAudio(AudioTypes.IntroSound);
    }

    void Start()
    {
        weaponCanvasGroup.alpha = 0f;
        LeanTween.delayedCall(3, () =>
        {
            LeanTween.alphaCanvas(weaponCanvasGroup, 1f, 1).setEase(LeanTweenType.easeInOutSine);
        });

        LeanTween.alphaCanvas(weaponCanvasGroup, 1f, 1).setEase(LeanTweenType.easeInOutSine);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void AddWeaponUI(WeaponBehavior weapon)
    {
        var weaponReferenceUI = Instantiate(weapon.weaponUiPrefab, weaponPanel.transform.position, Quaternion.identity);
        weapon.weaponUiReference = weaponReferenceUI;
        weapon.weaponUiReference.Init(weapon.weaponName, $"{weapon.ammoInChaimber}/{weapon.clipSize} : {weapon.ammoAmount}");
        weapon.weaponUiReference.transform.SetParent(weaponPanel.transform, false);
    }

    public void StartGame()
    {
        Debug.Log("GAme Started");
        OnGameStart?.Invoke();
        playerObject.SetActive(true);
        ToggleMainMenuCanvasGroup(false);
        AudioManager.Instance.PlayAudio(AudioTypes.BattleSound);
        AudioManager.Instance.PlayAudio(AudioTypes.IntroSound, false);
    }

    void OnReloadGame()
    {
        StartCoroutine(WaitABitForReloadGame());
    }

    IEnumerator WaitABitForReloadGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    void ToggleMainMenuCanvasGroup(bool state)
    {
        if (state)
        {
            LeanTween.alphaCanvas(mainMenuCanvasGroup, 1f, 0.5f).setEase(LeanTweenType.easeInOutSine);

        } else
        {
            LeanTween.alphaCanvas(mainMenuCanvasGroup, 0f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        }
    }

}
