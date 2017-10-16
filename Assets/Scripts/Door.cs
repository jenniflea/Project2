using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public Text helpText;
    private Animator animator;
    private bool DoorIsOpen = false;
    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    private void Awake() {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        animator.enabled = false;
        DoorIsOpen = false;
        meshRenderer.enabled = false;
        audioSource.Pause();
	}

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!DoorIsOpen) return;

        helpText.text = "Onto the Next Level!";
        StartCoroutine("WaitToLoad");
    }

    public void OpenDoor() {
        helpText.text = "Door opened!";
        animator.enabled = true;
        DoorIsOpen = true;
        meshRenderer.enabled = true;
        audioSource.Play();
    }

    IEnumerator WaitToLoad() {
        string nextScene = "";
        if (SceneManager.GetActiveScene().name == "Tutorial 1")
            nextScene = "Tutorial 2";
        else if (SceneManager.GetActiveScene().name == "Tutorial 2")
            nextScene = "Room 1";
        else if (SceneManager.GetActiveScene().name == "Room 1")
            nextScene = "Room 2";
        else
            nextScene = "Tutorial 1";

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
    }
}
