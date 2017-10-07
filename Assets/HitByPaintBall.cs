using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByPaintBall : MonoBehaviour {

    public GameObject PaintBallSplat;

    private void OnTriggerEnter(Collider other) {
        Vector3 position = other.transform.position;
        Instantiate(PaintBallSplat, position, transform.rotation);
        Destroy(other.gameObject);
    }
}
