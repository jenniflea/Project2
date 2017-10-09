using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int currentHealth;

    private void Start() {
        currentHealth = 5;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        if (collision.gameObject.GetComponent<EnemyMovement>().isExposed) return;
        currentHealth--;

        if (currentHealth <= 0) {
            Debug.Log("Out of health!");
            SceneManager.LoadScene("Main");
        }
    }
}
