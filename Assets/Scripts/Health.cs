﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public int currentHealth = 5;
    public Text healthText;
    public Text helpText;
    public GameObject Panel;
    public bool TutorialMode = false;

    private bool invincible = false;

    private void Start() {
        healthText.text = "Health: ";
        if (TutorialMode)
            healthText.text += "∞";
        else
            healthText.text += currentHealth.ToString();
    }

    private void OnCollisionEnter(Collision collision) {
        LowerHealth(collision);
    }

    private void OnCollisionStay(Collision collision) {
        LowerHealth(collision);
    }

    private void LowerHealth(Collision collision) {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        if (collision.gameObject.GetComponent<EnemyMovement>().isExposed) return;
        if (invincible) return;

        if (!TutorialMode) {
            currentHealth--;

            if (currentHealth < 0)
                currentHealth = 0;

            healthText.text = "Health: " + currentHealth;
        }
        StartCoroutine("Invincibility", collision);

        if (currentHealth <= 0) {
            Debug.Log("Out of health!");
            helpText.text = "Out of Health!";
            StartCoroutine("WaitToLoad");
        }
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
        SceneManager.LoadScene("Main");

    }
}
