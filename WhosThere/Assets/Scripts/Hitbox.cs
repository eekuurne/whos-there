using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

    [SerializeField] Character owner;

    public void TakeDamage(int damage, Transform shooter) {
        owner.TakeDamage(damage, shooter);
    }
}
