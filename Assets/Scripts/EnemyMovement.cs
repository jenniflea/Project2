using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public GameObject PaintPrefab;

    private Vector3 goal1;
    private int counter = 0;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        goal1 = new Vector3(30, 5, 30);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        counter++;
        rb.velocity = goal1*Time.deltaTime;

        if (counter == 120) {
            Instantiate(PaintPrefab, transform.position + Vector3.down*2.5f, transform.rotation);
            counter = 0;
        }

	}
}
