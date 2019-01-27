using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    [SerializeField] GameObject gun;
    [SerializeField] Character owner;

    [SerializeField] int shootingDamage = 1;

    public void Shoot() {
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
    }
}
