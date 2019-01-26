using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField] int startingHealth = 5;

    bool dead = false;

    int healthRemaining;

    protected void InitCharacter() {
        healthRemaining = startingHealth;
    }

    public virtual void TakeDamage(int damage, Transform shooter) {
        healthRemaining -= damage;
        Debug.Log("Hit character. Health remaining: " + healthRemaining);
        if (healthRemaining <= 0 && !dead) {
            Die();
        }
    }

    public virtual void Die() {
        Debug.Log("Character died!");
        dead = true;
    }
}
