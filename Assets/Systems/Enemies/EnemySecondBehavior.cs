using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySecondBehavior : EnemyBehavior
{

    public float chaseDistance = 10f;
    public float idleDistance = 1f;
    public float runAwayMultiplier = 4f;

    [SerializeField] Transform[] runAway;
    [SerializeField] bool isRunningAway;
    [SerializeField] Transform runawaySpot;


    [SerializeField] Transform ButtlePositionL;
    [SerializeField] Transform ButtlePositionR;

    [SerializeField] Molotov molotovPrefab;

    [SerializeField] int throwables = 2;

    public float throwSpeed = 10f;
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
            else if (distanceToPlayer > idleDistance && throwables > 0)
            {
                Debug.Log("MOL: ATTACK");
                agent.ResetPath();
                //animator.SetBool("Attack", true);

                if (PlayerHive.Instance.isJumpSmashInvulnerability || PlayerHive.Instance.playerInSaw)
                    return;

                animator.SetTrigger("AttackTrigger");
                animator.SetBool("Running", false);
            }
            else
            {
                if (isRunningAway)
                {
                    animator.SetBool("Running", true);
                    return;
                }
                    
                isRunningAway = true;
                Debug.Log("MOL: RUN AWAY");

                // Player is close, move in opposite direction
                // Vector3 oppositeDirection = transform.position - PlayerHive.Instance.transform.position;
                /*Vector3 oppositeDirection = (transform.position - PlayerHive.Instance.transform.position).normalized * runAwayMultiplier;

                agent.SetDestination(transform.position + oppositeDirection.normalized);
                runPosition.transform.position = transform.position + oppositeDirection.normalized;*/

                runawaySpot = runAway[Random.Range(0, runAway.Length - 1)];

                agent.SetDestination(runawaySpot.transform.position);
                //animator.SetBool("Attack", false);
                animator.SetBool("Running", true);

            }
        }
    }

    void ThrowObject()
    {
        if (PlayerHive.Instance == null)
        {
            return;
        }
        if (PlayerHive.Instance.isJumpSmashInvulnerability || PlayerHive.Instance.playerInSaw)
            return;

        var thrownObject = Instantiate(molotovPrefab, transform.position, Quaternion.identity);
        thrownObject.target = PlayerHive.Instance.transform;

        Vector3 targetPosition = PlayerHive.Instance.transform.position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        float timeToReachTarget = throwSpeed / Physics.gravity.magnitude;
        Vector3 initialVelocity = direction * throwSpeed - 0.5f * Physics.gravity * timeToReachTarget;

        thrownObject.transform.LookAt(targetPosition);

        LeanTween.move(thrownObject.gameObject, targetPosition, timeToReachTarget)
            .setEase(LeanTweenType.easeOutQuad);

        throwables--;
        if (throwables == 0)
        {
            StartCoroutine(Reload());
        }
        
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f);
        throwables = 2;
    }

    protected override void PerformAttack()
    {
        // base.PerformAttack();
        ThrowObject();
    }

}
