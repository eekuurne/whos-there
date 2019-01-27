using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCorpse : MonoBehaviour {
    
    public GameObject head;
    public GameObject spine;
    public GameObject hips;
    public Material startMaterial;
    public Material fadeMaterial;

    void Start() {
        startMaterial = GetComponentInChildren<Renderer>().material;
    }

    public void ChangeToFadeMaterial() {
        GetComponent<Renderer>().material = fadeMaterial;
    }

    void OnDestroy() {
        Destroy(startMaterial);
    }
}
