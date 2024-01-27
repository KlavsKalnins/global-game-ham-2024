using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    [SerializeField] TMP_Text debugWeapons;
    [SerializeField] List<WeaponBehavior> weapons;
    //[SerializeField] List<WeaponUI> weaponsUI; // var WeaponUI
    [SerializeField] bool isShooting;
    private List<Coroutine> shootCoroutines = new List<Coroutine>();

    [SerializeField] ParticleSystem particle;
    [SerializeField] float particleWaitTillRun = 0.2f;
    void Start()
    {
        debugWeapons.text = "";
        weapons.ForEach(w =>
        {
            debugWeapons.text += $"Weapon:{w.name} Reload:{w.isReloading} ammo:{w.ammoAmount} clip:{w.clipSize} chamber:{w.ammoInChaimber}\n";
        });
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isShooting = true;
        }
        else if (Input.GetMouseButton(1))
        {
            return;
        }
        else
        {
            isShooting = false;
        }

        if (isShooting)
        {
            LeanTween.delayedCall(particleWaitTillRun, () =>
            {
                if (particle != null)
                {
                    particle.Play();
                }
            });
            ShootWeapons();
        }
        else
        {
            StopShootCoroutines();
        }
    }

    void ShootWeapons()
    {
        PlayerManager.Instance.animatorUpperBody.SetBool("Shoot", true);
        debugWeapons.text = "";
        weapons.ForEach(w =>
        {
            Coroutine coroutine = StartCoroutine(w.Shoot());
            shootCoroutines.Add(coroutine);
            debugWeapons.text += $"{w.name} Reload:{w.isReloading} ammo:{w.ammoAmount} clip:{w.clipSize} chamber:{w.ammoInChaimber}\n";
        });
    }

    void StopShootCoroutines()
    {
        PlayerManager.Instance.animatorUpperBody.SetBool("Shoot", false);
        shootCoroutines.ForEach(coroutine =>
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        });
        shootCoroutines.Clear();
    }
}
