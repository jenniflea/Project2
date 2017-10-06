using UnityEngine;

public class PaintGun : MonoBehaviour {

    public GameObject bulletPrefab;

    private GameObject bullet;
    private Rigidbody bulletRB;

    public void ShootBullet() {
        Debug.Log("Bullet shot");
        Debug.Log(transform.position + -3 * transform.forward);
        Debug.Log(transform.position);
        bullet = Instantiate(bulletPrefab, transform.position + -3*transform.forward, transform.rotation*Quaternion.Euler(0,0,90));
        bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(-10*transform.forward);
    }
}
