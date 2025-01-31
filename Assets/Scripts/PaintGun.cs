﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PaintGun : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject targetPrefab;
    public int totalBullets = 15;
    public Text numBulletsLeft;
    public Text helpText;
    public static PaintGun instance;
    public bool TutorialMode = false;

    private bool bulletShot = false;
    private Rigidbody bulletRB;
    private GameObject target;
    private static int numBulletsUsed;
    RaycastHit raycastHit = new RaycastHit();

    public static bool HasBulletsRemaining {
        get {
            return numBulletsUsed < instance.totalBullets;
        }
    }

    private void Start() {
        target = Instantiate(targetPrefab, transform.position, targetPrefab.transform.rotation);
        numBulletsUsed = 0;
        if (TutorialMode)
            numBulletsLeft.text = "∞";
        else
            numBulletsLeft.text = totalBullets.ToString();
        numBulletsLeft.text += " Bullets";

        helpText.text = "";

        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void FixedUpdate() {
        // Show an estimate of where the bullet will go
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 150.0f)) {
            target.transform.position = raycastHit.point + raycastHit.normal*0.05f;
            target.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
        }
    }

    private IEnumerator BulletShot() {
        bulletShot = true;
        yield return new WaitForSeconds(0.5f);
        bulletShot = false;
    }

    public void ShootBullet() {
        if (numBulletsUsed >= totalBullets) return;
        if (bulletShot) return;

        var bullet = Instantiate(bulletPrefab, transform.position + 1.5f * transform.forward, transform.rotation * Quaternion.Euler(90, 0, 0));
        bullet.GetComponent<Bullet>().SetSplatLocation(raycastHit);
        UpdateNumBullets();
        StartCoroutine("BulletShot");
    }

    public static void NoBulletsLeft() {
        instance.helpText.text = "No more bullets remaining!";
        Debug.Log("No more bullets remaining! All enemies are not exposed.");
        instance.StartCoroutine("WaitToLoad");
    }

    void UpdateNumBullets() {
        if (TutorialMode) return;
        numBulletsUsed++;

        var bulletsRemaining = totalBullets - numBulletsUsed;

        numBulletsLeft.text = bulletsRemaining.ToString();

        if (bulletsRemaining == 1) {
            numBulletsLeft.text += " Bullet";
            numBulletsLeft.fontSize = 24;
            numBulletsLeft.color = Color.red;
            numBulletsLeft.fontStyle = FontStyle.Bold;
        } else
            numBulletsLeft.text += " Bullets";
    }

    IEnumerator WaitToLoad() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
