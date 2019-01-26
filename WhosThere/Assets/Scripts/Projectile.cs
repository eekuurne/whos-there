using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] float speed = 30;
    [SerializeField] int damage = 1;

    // Bullet gets destroyed in 5 seconds. Change to object pooling if there is time.
    float lifetime = 5;
    float startTime;

    Character shooter;

    void Start() {
        Destroy(gameObject, lifetime);
        startTime = Time.time;
    }

    void Update() {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
    }

    public void SetShooter(Character newShooter) {
        shooter = newShooter;
    }

    void OnTriggerEnter(Collider c) {
        Hitbox damageableObject = c.GetComponent<Hitbox>();
        // Player shooting at enemy
        if (damageableObject != null && c.gameObject.layer == 10) {
            // Play bullet hitting sound
            Debug.Log("Hit enemy!");
            damageableObject.TakeDamage(damage, shooter.transform);
            Destroy(gameObject);
        }
    }
}
