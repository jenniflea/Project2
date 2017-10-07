using UnityEngine;

public class PaintGun : MonoBehaviour {

    public GameObject bulletPrefab;

    private GameObject bullet;
    private Rigidbody bulletRB;

    public void ShootBullet() {
        if (bullet != null) return;
        bullet = Instantiate(bulletPrefab, transform.position + 1.5f*transform.forward, transform.rotation*Quaternion.Euler(90,0,0));
        bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(10000*transform.forward);
    }
}
