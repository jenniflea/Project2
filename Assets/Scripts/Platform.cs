using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public bool isExposed = false;
    public Material material;

    private BoxCollider boxCollider;

    private void Start() {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Expose(Color color) {
        MeshRenderer m = gameObject.AddComponent<MeshRenderer>();
        m.material = material;
        m.material.SetColor("_Color", color);
        isExposed = true;
        boxCollider.isTrigger = false;
    }
}
