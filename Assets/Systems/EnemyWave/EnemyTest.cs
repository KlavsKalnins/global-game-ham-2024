using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour, IHealthBehavior
{
    public static int aliveCount;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] Rigidbody rigidbody;

    [SerializeField] bool hasBeenStun;

    [SerializeField] HumanoidStatsSO stats;
    [SerializeField] int health;

    private bool IsAttackingPlayer;

    private void OnEnable()
    {
        aliveCount++;
    }
    private void OnDisable()
    {
        aliveCount--;
    }

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

        if (Input.GetKeyDown(KeyCode.O))
        {
            IsAttackingPlayer = !IsAttackingPlayer;
            agent.enabled = IsAttackingPlayer;
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
        int countTries = 0;
        hasBeenStun = true;
        agent.enabled = false;
        // Debug.Log($"KK magnitude: {rigidbody.velocity.magnitude}");
        while (rigidbody.velocity.magnitude > 3f)
        {
            Debug.Log($"KK: {countTries} {rigidbody.velocity.magnitude}");
            countTries += 1;
            if (countTries == 2)
            {
                break;
            }
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(0.1f);
        if (takePlayerDamage)
        {
            Damage(PlayerHive.Instance.GetDamageAmount(), true);
        }
        yield return new WaitForSeconds(0.1f);
        FinishDamage();
        yield return new WaitForSeconds(0.05f);
        // Debug.Log("WaitTillResetAgent");
        ResetAgent();
        yield return new WaitForSeconds(0.02f);
        agent.enabled = false;
        yield return new WaitForSeconds(0.02f);
        agent.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log($"KK: BULLET");
            Destroy(collision.gameObject);
            // collision.gameObject.SetActive(false);
            Damage(PlayerHive.Instance.GetDamageAmount(), true);
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

    public void Damage(int damage, bool finishInstantly = false, bool? shouldCallStun = null)
    {
        Debug.Log($"DAMAGE ENEMY {health} - {damage} = {health - damage}");
        health -= damage;
        if (finishInstantly)
        {
            FinishDamage();
        }
        if (shouldCallStun.HasValue && shouldCallStun.Value == true)
        {
            // already took damage from Inferface from other side
            StartCoroutine(WaitTillResetAgent(false));
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
