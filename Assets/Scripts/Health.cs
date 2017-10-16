using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int totalHealth = 5;
    public Text healthText;
    public Text helpText;
    public GameObject Panel;
    public bool TutorialMode = false;

    private bool invincible = false;
    private int currentHealth;
    private AudioSource audioSource;

    private void Start() {
        currentHealth = totalHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.Pause();

        healthText.text = "Health: ";
        if (TutorialMode)
            healthText.text += "∞";
        else
            healthText.text += currentHealth.ToString() + "/" + totalHealth.ToString();
    }

    private void OnCollisionEnter(Collision collision) {
        LowerHealth(collision);
    }

    private void OnCollisionStay(Collision collision) {
        LowerHealth(collision);
    }

    private void LowerHealth(Collision collision) {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        if (collision.gameObject.GetComponentInParent<EnemyMovement>().isExposed) return;
        if (invincible) return;

        audioSource.Play();

        if (!TutorialMode) {
            currentHealth--;

            if (currentHealth < 0)
                currentHealth = 0;

            healthText.text = "Health: " + currentHealth.ToString() + "/" + totalHealth.ToString();
        }

        StartCoroutine("HurtByEnemy");
        StartCoroutine("Invincibility", collision);

        if (currentHealth <= 0) {
            Debug.Log("Out of health!");
            helpText.text = "Out of Health!";
            StartCoroutine("WaitToLoad");
        }
    }

    IEnumerator HurtByEnemy() {
        PlayerMovement.hitByEnemy = true;
        yield return new WaitForFixedUpdate();
        gameObject.GetComponent<Rigidbody>().velocity = -400 * transform.forward;
        yield return new WaitForFixedUpdate();
        PlayerMovement.hitByEnemy = false;
    }

    IEnumerator Invincibility(Collision collision) {
        invincible = true;
        Color oldTextColor = healthText.color;
        int oldFontSize = healthText.fontSize;

        healthText.color = Color.red;
        healthText.fontSize = 24;
        healthText.fontStyle = FontStyle.Bold;

        helpText.text = "Hit by Invisible " + collision.gameObject.name + "!";
        for (int seconds = 0; seconds < 4; seconds++) {
            Panel.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Panel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        helpText.text = "";

        invincible = false;
        healthText.color = oldTextColor;
        healthText.fontSize = oldFontSize;
        healthText.fontStyle = FontStyle.Normal;
    }

    IEnumerator WaitToLoad() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
