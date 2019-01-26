using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public CharacterAnimation characterAnimation { get; private set; }

    public GameObject moveTarget;
    public RagdollCorpse ragdollCorpse;
    public float AttackRange = 10;
    IEnumerator attack;
    IEnumerator attackLoop;

    void Start() {
        characterAnimation = GetComponent<CharacterAnimation>();
        moveTarget = FindObjectOfType<Player>().gameObject;
        InitNavMeshAgent();
        InitCharacter();
        attack = Attack();
        attackLoop = AttackLoop();
        StartCoroutine(attackLoop);
    }

    void InitNavMeshAgent() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    void Update() {
        HandleMovement();
    }

    IEnumerator AttackLoop()
    {
        while(IsPlayerDead() != false)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            if (IsPlayerWithinRange())
            {
                StartCoroutine(attack);
            }
            if (IsPlayerDead())
            {
                StopAttackLoop();
            }
        }
    }

    private bool IsPlayerDead( )
    {
        return moveTarget.GetComponent<Player>().GetHealth() == 0;
    }

    void StopAttackLoop()
    {
        StopCoroutine(attackLoop);
    }

    IEnumerator Attack( )
    {
        agent.isStopped = true;
        // animation.Play();
        float animDuration = 1f;
        yield return new WaitForSecondsRealtime(animDuration);
        moveTarget.GetComponent<Player>().TakeDamage(1, transform);
    }

    private void StopAttack()
    {
        StopCoroutine(attack);
    }

    bool IsPlayerWithinRange()
    {
        return Vector3.Distance(moveTarget.transform.position,transform.position) < AttackRange;
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
        Instantiate(ragdollCorpse, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
