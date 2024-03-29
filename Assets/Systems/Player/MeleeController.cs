using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] float cooldownTime = 3;
    [SerializeField] float cooldownTimer = 0;
    [SerializeField] float force = 10f;

    [SerializeField] Rigidbody rigidbody;

    [SerializeField] MeleeUI meleeUI;
    public static int meleeDashDamage = 1;

    [SerializeField] ParticleSystem particle;
    [SerializeField] float particleWaitTillRun = 0.2f;

    [SerializeField] AudioSource meleeAudioSource;
    [SerializeField] float meleeAudioDelay = 0f;

    [SerializeField] PlayerSword sword;
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0)
        {
            meleeUI.StartReload(cooldownTime);
            cooldownTimer = cooldownTime;

            Vector3 forwardVector = transform.forward;

            rigidbody.AddForce(forwardVector * force, ForceMode.Impulse);
            PlayerManager.Instance.animatorUpperBody.SetTrigger("Sword");
            // sword.ToggleColliderState(true);
            LeanTween.delayedCall(particleWaitTillRun, () =>
            {
                if (particle != null)
                {
                    particle.Play();
                }
            });
            LeanTween.delayedCall(meleeAudioDelay, () =>
            {
                if (meleeAudioSource != null)
                {
                    meleeAudioSource.Play();
                }
            });
            StartCoroutine(PlayerHive.Instance.MeleeAction());
        }
    }

    public void MeleeAttackStart()
    {
        sword.ToggleColliderState(true);
    }

    public void MeleeAttackEnd()
    {
        sword.ToggleColliderState(false);
    }
}



/* UP SMASH


public class MeleeController : MonoBehaviour
{
    [SerializeField] float cooldownTime = 3;
    [SerializeField] float cooldownTimer = 0;
    [SerializeField] float force = 10f;

    [SerializeField] Rigidbody rigidbody;

    [SerializeField] MeleeUI meleeUI;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            meleeUI.StartReload(cooldownTime);
            cooldownTimer = cooldownTime;

            // Use transform.up instead of transform.forward
            Vector3 upwardVector = rigidbody.transform.up;

            rigidbody.AddForce(upwardVector * force, ForceMode.Impulse);
            StartCoroutine(PlayerHive.Instance.MeleeAction());
        }
    }
}


*/