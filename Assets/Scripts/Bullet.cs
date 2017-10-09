using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {

    public GameObject PaintBallSplatter;

    private Material material;

    // Use this for initialization
    private void Awake() {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    void Start () {
        Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        material.SetColor(Shader.PropertyToID("_Color"), color);
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) return;
        Vector3 position = collision.collider.ClosestPointOnBounds(transform.position + transform.up * -5);

        if (collision.gameObject.CompareTag("Enemy")) {
            PaintBallSplatter.transform.localScale = new Vector3(0.8f, 0.8f, 0);
            EnemyCounter.EnemyExposed();
            collision.gameObject.GetComponent<EnemyMovement>().isExposed = true;
        } else if (collision.gameObject.CompareTag("Floor"))
            PaintBallSplatter.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        else if (collision.gameObject.CompareTag("Ceiling")) {
            Destroy(gameObject);
            return;
        } else if (collision.gameObject.CompareTag("Platform"))
            PaintBallSplatter.transform.localScale = new Vector3(0.5f, 1, 0);
        else
            PaintBallSplatter.transform.localScale = new Vector3(0.1f, 0.2f, 0);

        foreach (ContactPoint contact in collision.contacts) {
            Vector3 normal = contact.normal * 0.02f;
            position += normal * 0.02f;
            var splat = Instantiate(PaintBallSplatter, position, Quaternion.LookRotation(normal), collision.gameObject.transform);
            splat.GetComponent<MeshRenderer>().materials[0].SetColor(Shader.PropertyToID("_Color"), material.color);
        }
        Destroy(gameObject);

        if (!PaintGun.HasBulletsRemaining && !EnemyCounter.AllEnemiesExposed) {
            Debug.Log("No more bullets remaining! All enemies are not exposed.");
            SceneManager.LoadScene("Main");
        }
    }
}
