using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour, IHealthBehavior
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Rigidbody rigidbody;

    [SerializeField] bool hasBeenStun;

    [SerializeField] HumanoidStatsSO stats;
    [SerializeField] int health;

    void Start()
    {
        health = stats.Health;
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null )
        {
            agent.SetDestination(PlayerHive.Instance.transform.position);
        }
    }

    void Update()
    {
        if (agent != null && !hasBeenStun && agent.isActiveAndEnabled)
        {
            agent.SetDestination(PlayerHive.Instance.transform.position);
        }
    }

    void ResetAgent()
    {
        agent.enabled = false;
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        
        rigidbody.isKinematic = false;
        agent.enabled = true;
        hasBeenStun = false;
    }

    IEnumerator WaitTillResetAgent(bool takePlayerDamage = true)
    {
        hasBeenStun = true;
        agent.enabled = false;
        // Debug.Log($"KK magnitude: {rigidbody.velocity.magnitude}");
        while (rigidbody.velocity.magnitude > 3f)
        {
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(0.25f);
        if (takePlayerDamage)
        {
            Damage(PlayerHive.Instance.GetDamageAmount(), true);
        }
        yield return new WaitForSeconds(0.25f);
        FinishDamage();
        yield return new WaitForSeconds(0.05f);
        // Debug.Log("WaitTillResetAgent");
        ResetAgent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Damage(PlayerHive.Instance.GetDamageAmount(), true);
            StartCoroutine(WaitTillResetAgent());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(WaitTillResetAgent(false));
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"KK: OnCollisionEnter Player {collision.gameObject.name}");
            if (!hasBeenStun && PlayerHive.Instance.isJumpSmashInvulnerability)
            {
                Debug.Log("probably collided from smash");
                Damage(JumpSmashController.jumpSmashDamage, true);
                StartCoroutine(WaitTillResetAgent());
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            return;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        Debug.Log($"KK: OnCollisionExit {collision.gameObject.name}");
        if (!hasBeenStun && PlayerHive.Instance.isMeleeInvulnerability)
        {
            Damage(MeleeController.meleeDashDamage, true);
            StartCoroutine(WaitTillResetAgent());
        }
    }

    public void Damage(int damage, bool finishInstantly = false)
    {
        Debug.Log($"DAMAGE ENEMY {health} - {damage} = {health - damage}");
        health -= damage;
        if (finishInstantly)
        {
            FinishDamage();
        }
    }

    public void FinishDamage()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
