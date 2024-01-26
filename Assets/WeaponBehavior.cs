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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Update()
    {
        
    }

    public IEnumerator Shoot()
    {
        Debug.Log($"KK: {isReloading}");

        if (!isReloading)
            Shoot();
        for (int i = 0; i < clipSize; i++)
        {
            if (ammoInChaimber != 0)
            {
                // spawn bullet
                ammoInChaimber -= 1;

                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                StartCoroutine(bullet.DeathTimer(range));
                bulletRb.AddForce(transform.forward * 50, ForceMode.Impulse);
                Debug.Log($"KK: spawned bullet");
                yield return new WaitForSeconds(fireRate);
            } 
            else
            {
                if (!isReloading)
                {
                    Debug.Log($"KK: Reloading");

                    StartCoroutine(Reloading());
                }
            }
        }
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
