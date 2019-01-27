using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    [SerializeField] GameObject gun;
    [SerializeField] Character owner;

    [SerializeField] int shootingDamage = 1;

    Animator anim;
    bool weaponUp = false;

    float nextAttackTime;
    float attackCooldown = 0.66f;

    void Start() {
        anim = GetComponent<Animator>();
        nextAttackTime = Time.time;
    }

    public void PullFingerGun() {
        weaponUp = true;
    }

    void Update() {
        anim.SetBool("WeaponUp", weaponUp);
        anim.speed = 1;
    }

    public void Shoot() {
        if (Time.time < nextAttackTime) {
            return;
        }
        nextAttackTime = Time.time + attackCooldown;
        anim.Play("FPS_Hands_Weapon_Shoot");
        // Layermask for layers 10 ("HitboxCollider") and 13 ("BreakableObject")
        int layerMask = (1 << 10) | (1 << 13);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
            Enemy damageableObject = hit.transform.GetComponent<Enemy>();
            if (damageableObject != null && hit.transform.gameObject.layer == 10) {
                // Play bullet hitting enemy sound
                damageableObject.TakeDamage(shootingDamage, owner.transform);
            } else if (damageableObject != null && hit.transform.gameObject.layer == 13) {
                // Destroy breakable object
            }
        }
    }

    public void HandPush() {
        Debug.Log("Hand push!");
        // Layermask for layers 10 ("HitboxCollider") and 13 ("BreakableObject")
        int layerMask = (1 << 10) | (1 << 13);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        Collider[] enemies =Physics.OverlapSphere(gun.transform.position, 180f, layerMask);
        foreach(Collider collider in enemies)
        {
            Enemy damageableObject = collider.gameObject.GetComponent<Enemy>();
            

            if (damageableObject != null && collider.transform.gameObject.layer == 10)
            {
                // Play bullet hitting enemy sound
                Vector3 forceDirection = (damageableObject.transform.position - owner.transform.position + Vector3.up).normalized; 
                StartCoroutine(damageableObject.ApplyPoke(0.25f, damageableObject.GetComponent<Rigidbody>(), 500, forceDirection, damageableObject.transform));

            }
            else if (damageableObject != null && collider.transform.gameObject.layer == 13)
            {
                // Destroy breakable object
            }
        }
    }

    
}
