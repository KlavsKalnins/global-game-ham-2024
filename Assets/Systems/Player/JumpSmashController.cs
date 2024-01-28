using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSmashController : MonoBehaviour
{
    public static JumpSmashController Instance;
    [SerializeField] float cooldownTime = 3;
    [SerializeField] float cooldownTimer = 0;
    [SerializeField] float force = 10f;
    [SerializeField] float forceUp = 10f;
    [SerializeField] float downForce = 10f;
    [SerializeField] float playDownForceAfterSeconds = 0.5f;

    [SerializeField] Rigidbody rigidbody;

    [SerializeField] MeleeUI meleeUI;

    public static int jumpSmashDamage = 2;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        PlayerManager.OnPlayerIsGrounded += OnLanded;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0) // Input.GetMouseButtonDown(0)
        {
            PlayerHive.Instance.isJumpSmashInvulnerability = true;
            meleeUI.StartReload(cooldownTime);
            cooldownTimer = cooldownTime;

            Vector3 impulseVector = transform.forward;
            impulseVector += transform.up * forceUp;

            rigidbody.AddForce(impulseVector * force, ForceMode.Impulse);
            LeanTween.delayedCall(playDownForceAfterSeconds, AddDownForceToRigidbody);
        }
    }

    void AddDownForceToRigidbody()
    {
        Debug.Log("Down force");
        // Apply the down force to the Rigidbody in the downward direction
        rigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);
    }

    [SerializeField] float smashRadius = 7f;

    void OnLanded(bool state)
    {
        if (PlayerHive.Instance.isJumpSmashInvulnerability == true && state == true)
        {
            Smash();
        }
    }

    public void Smash()
    {
        PlayerHive.Instance.isJumpSmashInvulnerability = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, smashRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemyy"))
            {
                IDamagable damagable = collider.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(jumpSmashDamage);
                }
            }
        }
    }
}