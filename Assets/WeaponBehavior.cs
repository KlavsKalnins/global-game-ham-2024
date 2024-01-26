using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public string name;
    public int ammoAmount;
    public int clipSize;
    public int ammoInChaimber;
    public int damage;
    public float fireRate;
    public int reloadSeconds;
    public bool isReloading;
    public float range;

    [SerializeField] BulletBehavior bulletPrefab;
    public WeaponUI weaponUiPrefab;
    public WeaponUI weaponUiReference;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void Start()
    {
        HudManager.Instance.AddWeaponUI(this);
    }

    public IEnumerator Shoot()
    {
        if (!isReloading)
            Shoot();
        for (int i = 0; i < clipSize; i++)
        {
            if (ammoInChaimber != 0)
            {
                // spawn bullet
                ammoInChaimber -= 1;
                weaponUiReference.UpdateAmmoText($"{ammoInChaimber}/{clipSize} : {ammoAmount}");
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                StartCoroutine(bullet.DeathTimer(range));
                bulletRb.AddForce(transform.forward * 50, ForceMode.Impulse);
                Debug.Log($"KK: spawned bullet");
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
    }
}
