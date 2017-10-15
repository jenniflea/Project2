using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPaintBlobs : MonoBehaviour {

    public GameObject PaintBlob;

    private void OnCollisionStay(Collision collision) {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        var position = collision.transform.position + -1 * gameObject.transform.forward * (collision.transform.localScale.z / 2);
        var paint = Instantiate(PaintBlob, position, Quaternion.LookRotation(gameObject.transform.up, gameObject.transform.forward));
        paint.GetComponent<Paint>().paintTrail = collision.gameObject.GetComponentInChildren<PaintTrail>();
        paint.GetComponent<MeshRenderer>().sortingOrder = paint.GetComponent<Paint>().paintTrail.Layer();
    }
}
