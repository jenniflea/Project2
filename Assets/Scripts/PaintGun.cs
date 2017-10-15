using UnityEngine;
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

    private Rigidbody bulletRB;
    private GameObject target;
    private static int numBulletsUsed;

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
        RaycastHit raycastHit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 150.0f)) {
            target.transform.position = raycastHit.point + raycastHit.normal*0.05f;
            target.transform.rotation = Quaternion.LookRotation(raycastHit.normal);
        }
    }

    public void ShootBullet() {
        if (numBulletsUsed >= totalBullets) return;

        var bullet = Instantiate(bulletPrefab, transform.position + 1.5f*transform.forward, transform.rotation*Quaternion.Euler(90, 0, 0));
        bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(10000*transform.forward);
        UpdateNumBullets();
    }

    public static void NoBulletsLeft() {
        instance.helpText.text = "No more bullets remaining!";
        Debug.Log("No more bullets remaining! All enemies are not exposed.");
        instance.StartCoroutine("WaitToLoad");
    }

    void UpdateNumBullets() {
        if (TutorialMode) return;
        numBulletsUsed++;
        numBulletsLeft.text = (totalBullets - numBulletsUsed) + " Bullets";
    }

    IEnumerator WaitToLoad() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");
    }

}
