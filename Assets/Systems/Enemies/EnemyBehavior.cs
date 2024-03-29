using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour, IDamagable
{
    public static int aliveCount;
    public static Action NoEnemiesAreAlive;

    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] StatsClass stats;

    [SerializeField] protected bool hasBeenStun;
    [SerializeField] float attackDistance = 1.5f;
    [SerializeField] float attackTime = 1f;
    [SerializeField] bool canTakeDamage = true;
    [SerializeField] float immortalSeconds = 0.2f;

    [SerializeField] bool canDroppable;
    [SerializeField] GameObject droppablePrefab;
    [SerializeField] float droppableChance = 0.2f;

    [SerializeField] IAttackable attackable;
    [SerializeField] ParticleSystem dieParticle;

    private void OnEnable()
    {
        aliveCount++;
    }

    private void OnDisable()
    {
        aliveCount--;
        if (aliveCount == 0)
        {
            NoEnemiesAreAlive?.Invoke();
        }
    }

    protected virtual void Awake()
    {
        Setup();
    }

    protected virtual void Setup()
    {
        attackable = GetComponent<IAttackable>();
        agent.speed = stats.WalkingSpeed;
        agent.stoppingDistance = attackDistance;
    }

    protected virtual void Start()
    {

        if (agent != null)
        {
            if (PlayerHive.Instance == null)
            {
                return;
            }
            agent.SetDestination(PlayerHive.Instance.transform.position);
        }
    }

    protected virtual void Update()
    {
        if (PlayerHive.Instance == null)
        {
            return;
        }
        if (agent != null && !hasBeenStun && agent.isActiveAndEnabled)
        {
            agent.SetDestination(PlayerHive.Instance.transform.position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                animator.SetBool("Attack", true);
                //animator.SetBool("Running", false);
            }
            else
            {
                animator.SetBool("Attack", false);
                //animator.SetBool("Running", true);
            }
        }
    }

    protected virtual void PerformAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerHive.Instance.transform.position);
        if (distanceToPlayer <= agent.stoppingDistance)
        {
            attackable.PerformAttack();
            //PlayerHive.Instance.TakeDamage(stats.Damage);
            // Debug.Log("Performing Attack! END");
        } 
        else
        {
            agent.SetDestination(PlayerHive.Instance.transform.position);

        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        stats.Health -= damage;
        StartCoroutine(StunWait());

        if (stats.Health <= 0)
        {
            Vector3 reff = transform.position;
            ParticleManager.Instance.SpawnParticle(reff, dieParticle);
            if (canDroppable)
                Droppable();
            EnemyAudio.Instance.PlayAudio();
            Destroy(gameObject);
        }

        LeanTween.delayedCall(immortalSeconds, () =>
        {
            canTakeDamage = true;
        });
    }

    IEnumerator StunWait()
    {
        hasBeenStun = true;
        agent.SetDestination(gameObject.transform.position);
        yield return new WaitForSeconds(0.5f);
        hasBeenStun = false;
    }

    protected virtual void Droppable()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= droppableChance)
        {
            Instantiate(droppablePrefab, transform.position, Quaternion.identity);
        }
    }
}
