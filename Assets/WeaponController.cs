using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] List<WeaponBehavior> weapons;
    [SerializeField] bool isShooting;
    private List<Coroutine> shootCoroutines = new List<Coroutine>();
    void Start()
    {
        
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
            ShootWeapons();
        }
        else
        {
            StopShootCoroutines();
        }
    }

    void ShootWeapons()
    {
        weapons.ForEach(weapon =>
        {
            Coroutine coroutine = StartCoroutine(weapon.Shoot());
            shootCoroutines.Add(coroutine);
        });
    }

    void StopShootCoroutines()
    {
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
