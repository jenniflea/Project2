using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByPaintBall : MonoBehaviour {

    public GameObject PaintBallSplatPrefab;

    private int index = 0;
    private GameObject[] Splats = new GameObject[100];

    private void OnTriggerEnter(Collider other) {
        Vector3 OnWallFace;
        OnWallFace.x = function(other.transform.position.x, transform.forward.x);
        OnWallFace.y = function(other.transform.position.y, transform.forward.y);
        OnWallFace.z = function(other.transform.position.z, transform.forward.z);

        // Find distance to wall
        float totalDistanceNeeded = Vector3.Magnitude(OnWallFace) / Mathf.Cos(Vector3.Angle(OnWallFace, -1 * other.transform.up)*Mathf.Deg2Rad) + 0.02f;

        Vector3 Position = other.transform.position + -1 * totalDistanceNeeded * other.transform.up;
        AddNewSplat(Position, transform.rotation);

        Destroy(other.gameObject);
        IncrementIndex();
    }

    private void AddNewSplat(Vector3 position, Quaternion rotation) {
        if (Splats[index] != null)
            Destroy(Splats[index]);
        Splats[index] = Instantiate(PaintBallSplatPrefab, position, rotation);
    }

    private void IncrementIndex() {
        if (index >= Splats.Length - 1)
            index = 0;
        else
            index++;
    }

    private float function(float position1, float position2) {
        return (position1 * position2) % 10;
    }
}
