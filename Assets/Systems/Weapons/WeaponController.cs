using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // [SerializeField] TMP_Text debugWeapons;
    [SerializeField] List<WeaponBehavior> weapons;
    [SerializeField] WeaponBehavior rangeWeapon;
    [SerializeField] bool isShooting;
    private List<Coroutine> shootCoroutines = new List<Coroutine>();

    [SerializeField] ParticleSystem particle;
    [SerializeField] float particleWaitTillRun = 0.2f;

    /*[SerializeField] AudioSource shootAudioSource;
    [SerializeField] float shootAudioDelay = 0f;*/
    void Start()
    {
        /*debugWeapons.text = "";
        weapons.ForEach(w =>
        {
            debugWeapons.text += $"Weapon:{w.name} Reload:{w.isReloading} ammo:{w.ammoAmount} clip:{w.clipSize} chamber:{w.ammoInChaimber}\n";
        });*/
    }

    void PlayParticle()
    {
        if (rangeWeapon.isReloading) return;
        LeanTween.delayedCall(particleWaitTillRun, () =>
        {
            if (particle != null)
            {
                particle.Play();
            }
        });
    } 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            PlayParticle();
        }
        else if (Input.GetMouseButton(0))
        {
            PlayParticle();
            return;
        }
        else
        {
            isShooting = false;
            // shootAudioSource.Stop();
        }
        

        if (isShooting && !rangeWeapon.isReloading)
        {
            
            /*LeanTween.delayedCall(shootAudioDelay, () =>
            {
                if (shootAudioSource != null)
                {
                    shootAudioSource.Play();
                }
            });*/
            ShootWeapons();
        }
        else
        {
            StopShootCoroutines();
        }
    }

    void ShootWeapons()
    {
        PlayParticle();
        PlayerManager.Instance.animatorUpperBody.SetBool("Shoot", true);

        Coroutine coroutine = StartCoroutine(rangeWeapon.Shoot());
        shootCoroutines.Add(coroutine);

        /*weapons.ForEach(w =>
        {
            Coroutine coroutine = StartCoroutine(w.Shoot());
            shootCoroutines.Add(coroutine);
            debugWeapons.text += $"{w.name} Reload:{w.isReloading} ammo:{w.ammoAmount} clip:{w.clipSize} chamber:{w.ammoInChaimber}\n";
        });*/
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
