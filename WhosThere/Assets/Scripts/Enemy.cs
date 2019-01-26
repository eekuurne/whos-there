using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public CharacterAnimation characterAnimation { get; private set; }

    public GameObject moveTarget;
    public RagdollCorpse ragdollCorpse;

    void Start() {
        characterAnimation = GetComponent<CharacterAnimation>();
        moveTarget = FindObjectOfType<Player>().gameObject;
        InitNavMeshAgent();
        InitCharacter();
    }

    void InitNavMeshAgent() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    void Update() {
        HandleMovement();
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
