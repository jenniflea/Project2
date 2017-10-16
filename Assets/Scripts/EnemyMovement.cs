using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [Tooltip("x and z axes of the starting position of the enemy")]
    public Vector2 startingPosition; 

    [Tooltip("x and z axes of the ending position of the enemy")]
    public Vector2 endingPosition;
    public float speed;
    public bool isExposed = false;

    private Vector3 startingPos;
    private Vector3 endingPos;
    private bool isMovingToEndPos;
    private PaintTrail paintTrail;
    private bool addBlob = false;
    private Rigidbody rb;

    private Vector3 CurrentGoal {
        get {
            if (isMovingToEndPos) return endingPos;
            else return startingPos;
        }
    }

    private void Awake() {
        isMovingToEndPos = true;
        paintTrail = GetComponent<PaintTrail>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        startingPos = new Vector3(startingPosition.x, transform.position.y, startingPosition.y);
        endingPos = new Vector3(endingPosition.x, transform.position.y, endingPosition.y);
        transform.position = startingPos;
        GetComponent<AudioSource>().Pause();
        StartCoroutine("Count");
    }

    private void FixedUpdate() {
        if (Vector3.Distance(transform.position, CurrentGoal) / 100 < 0.001)
        {
            isMovingToEndPos = !isMovingToEndPos;
            paintTrail.ChangeColor();
        }
        else
            rb.velocity = (CurrentGoal - transform.position).normalized * speed;

        if (addBlob)
            paintTrail.AddToTrail();
    }

    private IEnumerator Count() {
        while(true) {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            addBlob = true;
            yield return new WaitForFixedUpdate();
            addBlob = false;
        }
    }
}
