using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    [SerializeField] GameObject gun;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Projectile projectile;
    [SerializeField] Character owner;

    public void Shoot() {
        Projectile newProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation) as Projectile;
        newProjectile.SetShooter(owner);
    }
}
