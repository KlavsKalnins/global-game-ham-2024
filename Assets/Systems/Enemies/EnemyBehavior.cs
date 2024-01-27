using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour, IDamagable
{
    public static int aliveCount;
    public static Action NoEnemiesAreAlive;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] StatsClass stats;
    

    [SerializeField] bool hasBeenStun;
    [SerializeField] float attackDistance = 1.5f;
    [SerializeField] float attackTime = 1f;
    [SerializeField] bool canTakeDamage = true;
    [SerializeField] float immortalSeconds = 0.2f;

    [SerializeField] GameObject droppablePrefab;
    [SerializeField] float droppableChance = 0.2f;

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

    private void Awake()
    {
        Setup();
    }

    void Setup()
    {
        agent.speed = stats.WalkingSpeed;
        agent.stoppingDistance = attackDistance;
    }

    void Start()
    {
        if (agent != null)
        {
            agent.SetDestination(PlayerHive.Instance.transform.position);
        }
    }

    void Update()
    {
        if (agent != null && !hasBeenStun && agent.isActiveAndEnabled)
        {
            agent.SetDestination(PlayerHive.Instance.transform.position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                animator.SetBool("Attack", true);
            }
            else
            {
                // Debug.Log("running");
                animator.SetBool("Attack", false);
                
            }
        }
    }

    void PerformAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerHive.Instance.transform.position);
        if (distanceToPlayer <= agent.stoppingDistance)
        {
            PlayerHive.Instance.TakeDamage(stats.Damage);
            // Debug.Log("Performing Attack! END");
        } 
        else
        {
            agent.SetDestination(PlayerHive.Instance.transform.position);

        }
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;
        canTakeDamage = false;
        stats.Health -= damage;

        if (stats.Health <= 0)
        {
            Droppable();
            Destroy(gameObject);
        }

        LeanTween.delayedCall(immortalSeconds, () =>
        {
            canTakeDamage = true;
        });
    }

    void Droppable()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= droppableChance)
        {
            Instantiate(droppablePrefab, transform.position, Quaternion.identity);
        }
    }
}
