using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    [SerializeField] GameObject gun;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Projectile projectile;
    [SerializeField] Character owner;

    public void Shoot() {
        /*
        // Layermask for layers 10 ("Clickable") and 11 ("Target")
        int layerMask = (1 << 10) | (1 << 11);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
            if (hit.transform.tag == "Ground") {
                player.MoveToRaycastHit(hit);
            } else if (hit.transform.tag == "Enemy") {
                player.AttackToRaycastHit(hit);
            } else if (hit.transform.tag == "StartButton") {
                player.PressStartButton(hit);
            }
        }*/
    }
}
