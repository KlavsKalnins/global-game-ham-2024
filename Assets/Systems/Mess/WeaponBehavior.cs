using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public string weaponName;
    public int ammoAmount;
    public int clipSize;
    public int ammoInChaimber;
    public int damage;
    public float fireRate;
    public int reloadSeconds;
    public bool isReloading;
    public float range;
    public float bulletAutoExplodeTimer = 2f;
/*    float bulletFlightDistance = 10.0f;
    float bulletSpeed = 10.0f;*/

    [SerializeField] BulletBehavior bulletPrefab;
    public WeaponUI weaponUiPrefab;
    public WeaponUI weaponUiReference;

    [SerializeField] ParticleSystem particle;
    [SerializeField] float particleWaitTillRun = 0.5f;
    [SerializeField] bool isShooting;

    private Coroutine shootingCoroutine;

    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Start()
    {
        HudManager.Instance.AddWeaponUI(this);
        BulletsAudio.Instance.SetupBulletAudio(clipSize);
    }

    void PlayParticle()
    {
        // if (isReloading || !isShooting) return;
        LeanTween.delayedCall(particleWaitTillRun, () =>
        {
            if (particle != null)
            {
                particle.Play();
                Debug.Log("PLAY PARTICLE");
            }
        });
    }
    void StopParticle()
    {
        LeanTween.delayedCall(particleWaitTillRun, () =>
        {
            if (particle != null)
            {
                Debug.Log("STOP PARTICLE");
                particle.Stop();
            }
        });
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            //PlayParticle();
        }
        else if (Input.GetMouseButton(0))
        {
           // if (!particle.isPlaying)
            //    PlayParticle();
            //return;
        }
        else
        {
            isShooting = false;
            // shootAudioSource.Stop();
        }
        if (isShooting)
        {
            StartMyCoroutine();
        }
        else
        {
            StopMyCoroutine();
        }


        if (Input.GetKeyUp(KeyCode.R) && !isReloading)
        {
            StartReloading();
        }
    }

    public void StartMyCoroutine()
    {
        if (shootingCoroutine == null)
        {
            if (isReloading)
            {
                return;
            }
            shootingCoroutine = StartCoroutine(Shoot());
        }
    }
    public void StopMyCoroutine()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
            StopParticle();
            PlayerManager.Instance.animatorUpperBody.SetBool("Shoot", false);
        }
    }

    public IEnumerator Shoot()
    {
        PlayerManager.Instance.animatorUpperBody.SetBool("Shoot", true);
        PlayParticle();
        // PlayParticle();
        for (int i = 0; i < clipSize; i++)
        {
            if (ammoInChaimber != 0)
            {
                // spawn bullet
                ammoInChaimber -= 1;
                weaponUiReference.UpdateAmmoText($"{ammoInChaimber}/{clipSize} : {ammoAmount}");

                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                StartCoroutine(bullet.DeathTimer(bulletAutoExplodeTimer));
                BulletsAudio.Instance.PlayBulletAudio(i);
                bulletRb.AddForce(transform.forward * 50, ForceMode.Impulse);

                /* Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                 RaycastHit hit;

                 if (Physics.Raycast(ray, out hit))
                 {

                     if (Random.Range(0f, 1f) <= 0.0f) // 20% chance
                     {
                         var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                         Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                         StartCoroutine(bullet.DeathTimer(bulletAutoExplodeTimer));
                         bulletRb.AddForce(transform.forward * 50, ForceMode.Impulse);
                     } 
                     else
                     {
                         // Spawn bullet at player position
                         var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                         Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                         StartCoroutine(bullet.DeathTimer(bulletAutoExplodeTimer));

                         // Calculate direction towards the mouse position
                         Vector3 direction = (hit.point - transform.position).normalized;

                         // Apply force towards the mouse position
                         bulletRb.AddForce(direction * bulletFlightDistance * bulletSpeed, ForceMode.Impulse);
                         // bulletRb.AddForce(direction * 50, ForceMode.Impulse);
                     }

                 } 
                 else
                 {
                     var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                     Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                     StartCoroutine(bullet.DeathTimer(bulletAutoExplodeTimer));
                     bulletRb.AddForce(transform.forward * 50, ForceMode.Impulse);
                 }*/

                // Debug.Log($"KK: spawned bullet");
                if (ammoInChaimber == 0)
                {
                    StartReloading();
                }
                yield return new WaitForSeconds(fireRate);
            } 
            else
            {
                if (!isReloading)
                {
                    StartReloading();
                }
            }
        }
    }

    void StartReloading()
    {
        Debug.Log($"KK: Reloading");
        StopParticle();
        PlayerManager.Instance.animatorUpperBody.SetBool("Shoot", false);
        weaponUiReference.StartReload(reloadSeconds);
        // invoke reload so ui knows;
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadSeconds);
        if (ammoAmount >= clipSize)
        {
            ammoAmount -= clipSize;
        }
        ammoInChaimber = clipSize;
        isReloading = false;
        weaponUiReference.UpdateAmmoText($"{ammoInChaimber}/{clipSize} : {ammoAmount}");
    }
}
