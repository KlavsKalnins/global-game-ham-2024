using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] float cooldownTime = 3;
    [SerializeField] float cooldownTimer = 0;
    [SerializeField] float forwardForce = 10f;

    [SerializeField] Rigidbody rigidbody;

    [SerializeField] MeleeUI meleeUI;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            meleeUI.StartReload(cooldownTime);
            cooldownTimer = cooldownTime;
            Vector3 forwardVector = rigidbody.transform.forward;
            rigidbody.AddForce(forwardVector * forwardForce, ForceMode.Impulse);
            StartCoroutine(PlayerHive.Instance.MeleeAction());
        }
    }
}
