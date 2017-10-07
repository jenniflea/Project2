﻿using UnityEngine;

public class PaintGun : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject targetPrefab;

    private GameObject bullet;
    private Rigidbody bulletRB;
    private GameObject target;

    private void Start() {
        target = Instantiate(targetPrefab, transform.position, targetPrefab.transform.rotation);
    }

    private void FixedUpdate() {
        // Show an estimate of where the bullet will go
        RaycastHit raycastHit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 150.0f)) {
            target.transform.position = raycastHit.point;
            target.transform.rotation = raycastHit.transform.rotation;
        }
    }

    public void ShootBullet() {
        if (bullet != null) return;
        bullet = Instantiate(bulletPrefab, transform.position + 1.5f*transform.forward, transform.rotation*Quaternion.Euler(90, 0, 0));
        bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(10000*transform.forward);
    }
}
