using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRider : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) return;
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other) {
        if (!other.gameObject.CompareTag("Player")) return;
        other.transform.parent = null;
    }
}
