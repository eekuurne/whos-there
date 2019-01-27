using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField] int startingHealth = 5;

    protected bool dead = false;

    int healthRemaining;
    public float PokeTime = 0.5f;
    public GameObject CharacterModel;

    public int GetHealth()
    {
        return healthRemaining;
    }

    protected void InitCharacter() {
        healthRemaining = startingHealth;
    }

    public virtual void TakeDamage(int damage, Transform attacker) {
        StartCoroutine(AnimatePoke(attacker));
        healthRemaining -= damage;
        Debug.Log("Hit character. Health remaining: " + healthRemaining);
        if (healthRemaining <= 0 && !dead) {
            Die(attacker);
        }
    }

    IEnumerator AnimatePoke(Transform attacker)
    {
        var originalRotation = CharacterModel.transform.rotation;
        var finalRotation = Quaternion.AngleAxis(-15f, CharacterModel.transform.right);

        float timeElapsed = 0;

        while (timeElapsed < PokeTime / 2)
        {
            timeElapsed += Time.fixedDeltaTime;
            CharacterModel.transform.rotation = Quaternion.Slerp(CharacterModel.transform.rotation, finalRotation, timeElapsed / (PokeTime/2));
            yield return new WaitForFixedUpdate();
        }
        timeElapsed = 0;
        while (timeElapsed < PokeTime / 2)
        {
            timeElapsed += Time.fixedDeltaTime;
            CharacterModel.transform.rotation = Quaternion.Slerp(CharacterModel.transform.rotation, originalRotation, timeElapsed / (PokeTime/2));
            yield return new WaitForFixedUpdate();
        }
    }

    public virtual void Die(Transform attacker) {
        Debug.Log("Character died!");
        dead = true;
    }
}
