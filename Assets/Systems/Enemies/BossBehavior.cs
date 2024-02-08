using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : EnemyBehavior
{
    public float chaseDistance = 10f;
    public float idleDistance = 1f;
    public float runAwayMultiplier = 4f;

    [SerializeField] Transform[] runAway;
    [SerializeField] bool isRunningAway;
    [SerializeField] Transform runawaySpot;

    [SerializeField] Transform ButtlePositionL;
    [SerializeField] Transform ButtlePositionR;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (PlayerHive.Instance == null)
        {
            return;
        }

        if (isRunningAway)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                isRunningAway = false;
                runawaySpot = null;
            }
            if (runawaySpot)
            {
                return;
            }
        }

        if (agent != null && !hasBeenStun && agent.isActiveAndEnabled)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerHive.Instance.transform.position);

            if (distanceToPlayer > chaseDistance)
            {
                Debug.Log("MOL: CHASE");
                agent.SetDestination(PlayerHive.Instance.transform.position);
                //animator.SetBool("Attack", false);
                animator.SetBool("Running", true);
            }
            else if (distanceToPlayer > idleDistance)
            {
                Debug.Log("MOL: ATTACK");
                agent.ResetPath();
                //animator.SetBool("Attack", true);

                if (PlayerHive.Instance.isJumpSmashInvulnerability || PlayerHive.Instance.playerInSaw)
                    return;

                animator.SetBool("Running", false);
                animator.SetTrigger("AttackTrigger");
            }
            else
            {
               /* if (isRunningAway)
                {
                    animator.SetBool("Running", true);
                    return;
                }

                isRunningAway = true;
                Debug.Log("MOL: RUN AWAY");

                if (UnityEngine.Random.Range(0f, 1f) <= 0.3f)
                {
                    runawaySpot = runAway[Random.Range(0, runAway.Length - 1)];
                    agent.SetDestination(runawaySpot.transform.position);
                    animator.SetBool("Running", true);
                }*/

                // Player is close, move in opposite direction
                // Vector3 oppositeDirection = transform.position - PlayerHive.Instance.transform.position;
                /*Vector3 oppositeDirection = (transform.position - PlayerHive.Instance.transform.position).normalized * runAwayMultiplier;

                agent.SetDestination(transform.position + oppositeDirection.normalized);
                runPosition.transform.position = transform.position + oppositeDirection.normalized;*/
/*
                runawaySpot = runAway[Random.Range(0, runAway.Length - 1)];

                agent.SetDestination(runawaySpot.transform.position);
                animator.SetBool("Running", true);*/

            }
        }
    }

    protected override void PerformAttack()
    {
        base.PerformAttack();
    }

}
