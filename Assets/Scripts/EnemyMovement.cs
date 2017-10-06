using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject PaintPrefab;
    private Vector3 goal1;
    private Rigidbody rb;
    private float speed = 5.0f;

    private void Awake() {
        goal1 = new Vector3(30, 2, 30);
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        rb.velocity = (transform.position - goal1).normalized * speed;
        StartCoroutine("AddPaintTrail");
	}
	
    IEnumerator AddPaintTrail() {
        while (true) {
            Instantiate(PaintPrefab, transform.position + Vector3.down*2, transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
