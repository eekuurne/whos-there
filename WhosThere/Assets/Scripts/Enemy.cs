using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public CharacterAnimation characterAnimation { get; private set; }

    public GameObject moveTarget;

    void Start() {
        characterAnimation = GetComponent<CharacterAnimation>();
        InitNavMeshAgent();
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
            Debug.Log(agent.destination);
        }
        if (agent.remainingDistance > agent.stoppingDistance) {
            characterAnimation.Move(agent.desiredVelocity, false, false);
        } else {
            characterAnimation.Move(Vector3.zero, false, false);
        }
    }
}
