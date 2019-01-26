using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField] int startingHealth = 5;

    bool dead = false;

    int healthRemaining;

    void Start() {
        healthRemaining = startingHealth;
    }

    public virtual void TakeDamage(int damage) {
        healthRemaining -= damage;
        if (healthRemaining <= 0 && !dead) {
            Die();
        }
    }

    public virtual void Die() {
        Debug.Log("Character died!");
    }
}
