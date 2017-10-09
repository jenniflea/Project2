using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Animator animator;
    private bool DoorIsOpen = false;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
//        animator.StopPlayback();
	}

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (!DoorIsOpen) return;

        Debug.Log("You have won! Congratulations!");
    }

    public void OpenDoor() {
        animator.Play("Door");
        DoorIsOpen = true;
    }
}
