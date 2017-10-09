using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int currentHealth;
    public Text healthText;
    public Text helpText;

    private void Start() {
        currentHealth = 5;
        healthText.text = "Health: " + currentHealth;
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        if (collision.gameObject.GetComponent<EnemyMovement>().isExposed) return;
        currentHealth--;
        healthText.text = "Health: " + currentHealth;

        if (currentHealth <= 0) {
            Debug.Log("Out of health!");
            helpText.text = "Out of Health!";
            StartCoroutine("WaitToLoad");
        }
    }

    IEnumerator WaitToLoad() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main");

    }
}
