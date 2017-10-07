using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {

    [Tooltip("x and z axes of the starting position of the enemy")]
    public Vector2 startingPosition; 

    [Tooltip("x and z axes of the ending position of the enemy")]
    public Vector2 endingPosition;
    public float speed;

    private Rigidbody rb;
    private Vector3 startingPos;
    private Vector3 endingPos;
    private bool isMovingToEndPos;

    private Vector3 CurrentGoal {
        get {
            if (isMovingToEndPos) return endingPos;
            else return startingPos;
        }
    }

    private void Awake() {
       rb = GetComponent<Rigidbody>();
       isMovingToEndPos = true;
    }

    private void Start() {
        startingPos = new Vector3(startingPosition.x, transform.position.y, startingPosition.y);
        endingPos = new Vector3(endingPosition.x, transform.position.y, endingPosition.y);
        transform.position = startingPos;
        StartCoroutine("TimedMovement");
    }

    IEnumerator TimedMovement() {
        while (true) {

            // If other side is reached
            if (Vector3.Distance(transform.position, CurrentGoal) / 100 < 0.001) {
                rb.isKinematic = true;

                // Turn around 180 degrees
                for (int rotation = 0; rotation <= 180; rotation += 5) {
                    transform.rotation = Quaternion.Euler(0, rotation, 0);
                    yield return null;
                }

                rb.isKinematic = false;
                isMovingToEndPos = !isMovingToEndPos;
                Paint.ChangeColor();

                yield return null;
            
            // Else move toward other side
            } else
                rb.velocity = (CurrentGoal - transform.position).normalized * speed;

            yield return null;
        }
    }
}
