using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {

    public GameObject PaintBallSplatter;
    private Material material;
    private static int counter;

    // Use this for initialization
    private void Awake() {
        if (counter == -1)
            counter = 0;

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
            Counter.EnemyExposed(collidingGameObject, material.color);
            Destroy(gameObject);
            return;

        // Color platform
        } else if (collidingGameObject.CompareTag("Platform")) {
            Counter.PlatformExposed(collidingGameObject, material.color);
            Destroy(gameObject);
            return;
        
        // Don't splat on ceiling
        } else if (collidingGameObject.CompareTag("Ceiling")) {
            Destroy(gameObject);
            return;

        // Splat on floor
        } else if (collidingGameObject.CompareTag("Floor"))
            PaintBallSplatter.transform.localScale = new Vector3(0.1f, 0.1f, 0);

        // Splat on walls
        else
            PaintBallSplatter.transform.localScale = new Vector3(0.1f, 0.2f, 0);

        Vector3 position = collision.collider.ClosestPointOnBounds(transform.position + transform.up * -0.5f);
        ContactPoint c = collision.contacts[0];
        Vector3 normal = c.normal;

        var splat = Instantiate(PaintBallSplatter, position, Quaternion.LookRotation(normal), collision.gameObject.transform);
        splat.transform.localPosition = new Vector3(splat.transform.localPosition.x, splat.transform.localPosition.y, 0.501f + 0.001f*counter);
        splat.GetComponent<MeshRenderer>().materials[0].SetColor(Shader.PropertyToID("_Color"), material.color);
        splat.GetComponent<MeshRenderer>().sortingOrder = counter;
        counter++;
        Destroy(gameObject);

        if (!PaintGun.HasBulletsRemaining && !Counter.DoorIsOpen) {
            PaintGun.NoBulletsLeft();
        }
    }
}
