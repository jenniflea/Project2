using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {

    public GameObject PaintBallSplatter;

    private Material material;
    private static int counter;
    private Vector3 splatLocation;
    private GameObject objectHit;

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

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, splatLocation, 7);

        if (Vector3.Distance(transform.position, splatLocation) / 100 < 0.01f)
            SetSplat();
    }

    public void SetSplatLocation(RaycastHit raycastHit) {
        splatLocation = raycastHit.point + raycastHit.normal * 0.001f;
        objectHit = raycastHit.collider.gameObject;
    }

    private void SetSplat() {
        if (objectHit.CompareTag("Player")) return; // Just to be safe

        // Color enemy
        if (objectHit.CompareTag("Enemy"))
            Counter.EnemyExposed(objectHit, material.color);

        // Color platform
        else if (objectHit.CompareTag("Platform"))
            Counter.PlatformExposed(objectHit, material.color);

        // Splat on wall or floor
        else if (objectHit.CompareTag("Floor") || objectHit.CompareTag("Wall") || objectHit.CompareTag("Splat")) {
            var splat = Instantiate(PaintBallSplatter, splatLocation, Quaternion.LookRotation(objectHit.transform.forward));
            splat.GetComponent<MeshRenderer>().materials[0].SetColor(Shader.PropertyToID("_Color"), material.color);
            splat.GetComponent<MeshRenderer>().sortingOrder = counter;
            counter++;
        }

        Destroy(gameObject);

        if (!PaintGun.HasBulletsRemaining && !Counter.DoorIsOpen) {
            PaintGun.NoBulletsLeft();
        }
    }
}
