using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public CharacterAnimation characterAnimation { get; private set; }

    public GameObject moveTarget;
    public RagdollCorpse ragdollPrefab;
    public float AttackRange = 10;
    IEnumerator attackLoop;

    float attackCooldown = 2f;
    float nextAttack;

    void Start() {
        characterAnimation = GetComponent<CharacterAnimation>();
        moveTarget = FindObjectOfType<Player>().gameObject;
        InitNavMeshAgent();
        InitCharacter();
        //attackLoop = AttackLoop();
        //StartCoroutine(attackLoop);
        nextAttack = Time.time;
    }

    void InitNavMeshAgent() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    void Update() {
        HandleMovement();

        if (IsPlayerWithinRange() && Time.time > nextAttack) {
            StartCoroutine(MeleeAttack(1f));
        }
    }

    IEnumerator AttackLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            if (IsPlayerWithinRange() && Time.time > nextAttack)
            {
                Debug.Log("AttackCoroutine starts!");
                nextAttack = Time.time + attackCooldown;
                Debug.Log(nextAttack);
                agent.isStopped = true;
                // animation.Play();
                float animDuration = 1f;
                yield return new WaitForSeconds(animDuration);
                moveTarget.GetComponent<Player>().TakeDamage(1, transform);
                agent.isStopped = false;
            }
            if (IsPlayerDead())
            {
                StopAttackLoop();
            }
        }
    }

    IEnumerator MeleeAttack(float animationTime) {
        Debug.Log("AttackCoroutine starts!");
        nextAttack = Time.time + attackCooldown;
        agent.isStopped = true;
        // animation.Play();
        yield return new WaitForSeconds(animationTime);
        moveTarget.GetComponent<Player>().TakeDamage(1, transform);
        agent.isStopped = false;
    }

    private bool IsPlayerDead( )
    {
        return moveTarget.GetComponent<Player>().GetHealth() == 0;
    }

    void StopAttackLoop()
    {
        StopCoroutine(attackLoop);
    }

    bool IsPlayerWithinRange()
    {
        return Vector3.Distance(moveTarget.transform.position, transform.position) < AttackRange;
    }

    protected void HandleMovement() {
        if (moveTarget != null) {
            agent.SetDestination(moveTarget.transform.position);
        }
        if (agent.remainingDistance > agent.stoppingDistance) {
            characterAnimation.Move(agent.desiredVelocity, false, false);
        } else {
            characterAnimation.Move(Vector3.zero, false, false);
        }
    }

    public override void Die(Transform attacker) {
        dead = true;
        FindObjectOfType<HomeManager>().EnemyDies(transform, attacker, ragdollPrefab);
    }
}
