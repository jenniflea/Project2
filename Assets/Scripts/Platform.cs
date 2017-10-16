using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public bool isExposed;
    public GameObject shadow;

    private RaycastHit raycastHit;
    private LayerMask layerMask;

    private void Start() {
        isExposed = false;

        shadow = Instantiate(shadow, transform.position, Quaternion.LookRotation(Vector3.up));
        shadow.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.z, 0);

        GetComponent<AudioSource>().Pause();

        layerMask = (1 << LayerMask.NameToLayer("Platform")) + (1 << LayerMask.NameToLayer("Ignore Raycast"));
        layerMask = ~layerMask;
    }

    private void FixedUpdate() {
        if (Physics.Raycast(transform.position, Vector3.up * -1, out raycastHit, 100.0f, layerMask)) {
            shadow.transform.position = raycastHit.point + raycastHit.normal * 0.01f;
        }

    }
}
