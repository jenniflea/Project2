using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        var collidingGameObject = collision.gameObject;
        if (collidingGameObject.CompareTag("Player")) return;

        // Color enemy
        if (collidingGameObject.CompareTag("Enemy")) {
            EnemyCounter.EnemyExposed(collidingGameObject, material.color);
            Destroy(gameObject);
            return;
        
        // Color platform
        } else if (collidingGameObject.CompareTag("Platform")) {
            if (collidingGameObject.GetComponent<MeshRenderer>() != null) return;
            MeshRenderer m = collidingGameObject.AddComponent<MeshRenderer>();
            m.material = PaintBallSplatter.GetComponent<MeshRenderer>().sharedMaterials[0];
            m.material.SetColor("_Color", material.color);
            Destroy(gameObject);
            return;
        
        // Don't splat on ceiling
        } else if (collision.gameObject.CompareTag("Ceiling")) {
            Destroy(gameObject);
            return;

        // Splat on floor
        } else if (collision.gameObject.CompareTag("Floor"))
            PaintBallSplatter.transform.localScale = new Vector3(0.1f, 0.1f, 0);

        // Splat on ceiling
        else
            PaintBallSplatter.transform.localScale = new Vector3(0.1f, 0.2f, 0);

        Vector3 position = collision.collider.ClosestPointOnBounds(transform.position + transform.up * -0.5f);
        ContactPoint c = collision.contacts[0];
        Vector3 normal = c.normal;

        var splat = Instantiate(PaintBallSplatter, position, Quaternion.LookRotation(normal), collision.gameObject.transform);
        // Set splat so that it shows up properly on the surface
        splat.transform.localPosition = new Vector3(splat.transform.localPosition.x, splat.transform.localPosition.y, 0.51f);
        splat.GetComponent<MeshRenderer>().materials[0].SetColor(Shader.PropertyToID("_Color"), material.color);
        Destroy(gameObject);

        if (!PaintGun.HasBulletsRemaining && !EnemyCounter.AllEnemiesExposed) {
            PaintGun.NoBulletsLeft();
        }
    }
}
