using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public bool isExposed;
    public bool hasDynamicShadow = false;
    public GameObject shadowPrefab;

    private GameObject shadow;

    private void Start() {
        isExposed = false;

        if (!hasDynamicShadow) return;
        shadow = Instantiate(shadowPrefab, transform.position, transform.rotation);
        shadow.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
    }

    private void FixedUpdate() {
        if (!hasDynamicShadow) return;
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.right * -1, out raycastHit, 10.0f)) {
            shadow.transform.position = raycastHit.point + raycastHit.normal * 0.05f;
            shadow.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
        }

    }
}
