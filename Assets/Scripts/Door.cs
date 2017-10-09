using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public Text helpText;
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

        helpText.text = "You have won! Congratulations!";
        Debug.Log("You have won! Congratulations!");
        StartCoroutine("WaitToLoad");
    }

    public void OpenDoor() {
        helpText.text = "Door opened!";
        animator.enabled = true;
        DoorIsOpen = true;
    }

    IEnumerator WaitToLoad() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");
    }
}
