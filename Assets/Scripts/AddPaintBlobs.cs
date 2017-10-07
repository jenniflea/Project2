using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPaintBlobs : MonoBehaviour {

    public GameObject PaintBlob;

    private void OnCollisionStay(Collision collision) {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        var position = collision.transform.position + -1 * collision.transform.up * collision.transform.localScale.z / 2;
        Instantiate(PaintBlob, position, collision.transform.rotation);
    }
}
