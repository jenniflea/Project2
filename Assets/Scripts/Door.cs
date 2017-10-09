using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    private Animator animator;
    private bool DoorIsOpen = false;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        animator.enabled = false;
        DoorIsOpen = false;
	}

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (!DoorIsOpen) return;

        Debug.Log("You have won! Congratulations!");
        SceneManager.LoadScene("Main");
    }

    public void OpenDoor() {
        animator.enabled = true;
        DoorIsOpen = true;
    }
}
