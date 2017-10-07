using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByPaintBall : MonoBehaviour {

    public GameObject PaintBallSplat;

    private void OnTriggerEnter(Collider other) {
        Vector3 OnWallFace;
        OnWallFace.x = function(other.transform.position.x, transform.forward.x);
        OnWallFace.y = function(other.transform.position.y, transform.forward.y);
        OnWallFace.z = function(other.transform.position.z, transform.forward.z);

        // Find distance to wall
        float totalDistanceNeeded = Vector3.Magnitude(OnWallFace) / Mathf.Cos(Vector3.Angle(OnWallFace, -1 * other.transform.up)*Mathf.Deg2Rad);
        GameObject splat = Instantiate(PaintBallSplat, other.transform.position + -1 * totalDistanceNeeded * other.transform.up, transform.rotation);
        Destroy(other.gameObject);
        StartCoroutine("DestroyAfterAwhile", splat);
    }

    private float function(float position1, float position2) {
        return (position1 * position2) % 10;
    }

    IEnumerator DestroyAfterAwhile(GameObject g) {
        yield return new WaitForSeconds(15);
        Destroy(g);
    }
}
