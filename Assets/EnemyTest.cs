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

    IEnumerator WaitTillResetAgent()
    {
        hasBeenStun = true;
        agent.enabled = false;
        Debug.Log($"KK magnitude: {rigidbody.velocity.magnitude}");
        while (rigidbody.velocity.magnitude > 3f)
        {
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(0.25f);
        Damage(PlayerHive.Instance.GetDamageAmount());
        yield return new WaitForSeconds(0.25f);
        Debug.Log("WaitTillResetAgent");
        ResetAgent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(WaitTillResetAgent());
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            return;
        }
        Debug.Log($"KK: OnCollisionExit {collision.gameObject.name}");
        if (!hasBeenStun && PlayerHive.Instance.isMeleeInvulnerability)
        {
            StartCoroutine(WaitTillResetAgent());
        }
    }

    public void Damage(int damage)
    {
        Debug.Log($"DAMAGE ENEMY {health} - {damage} = {health - damage}");
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        //humanStats
    }
}
