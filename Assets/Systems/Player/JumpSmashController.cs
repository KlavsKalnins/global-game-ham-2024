using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSmashController : MonoBehaviour
{
    [SerializeField] float cooldownTime = 3;
    [SerializeField] float cooldownTimer = 0;
    [SerializeField] float force = 10f;
    [SerializeField] float forceUp = 10f;
    [SerializeField] float downForce = 10f;
    [SerializeField] float playDownForceAfterSeconds = 0.5f;

    [SerializeField] Rigidbody rigidbody;

    [SerializeField] MeleeUI meleeUI;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            meleeUI.StartReload(cooldownTime);
            cooldownTimer = cooldownTime;

            Vector3 impulseVector = transform.forward;
            impulseVector += transform.up * forceUp;

            StartCoroutine(PlayerHive.Instance.MeleeAction());

            rigidbody.AddForce(impulseVector * force, ForceMode.Impulse);
            StartCoroutine(PlayerHive.Instance.MeleeAction());
            LeanTween.delayedCall(playDownForceAfterSeconds, AddDownForceToRigidbody);
        }
    }

    void AddDownForceToRigidbody()
    {
        Debug.Log("Down force");
        // Apply the down force to the Rigidbody in the downward direction
        rigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);
    }
}