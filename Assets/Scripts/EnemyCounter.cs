using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {

    public static int totalEnemies;
    public GameObject Door;
    public static EnemyCounter instance;
    public Material enemyMaterial;

    public static bool AllEnemiesExposed {
        get {
            return exposedEnemies >= totalEnemies;
        }
    }

    private static int exposedEnemies;
    private Door door;

    private void Awake() {
        door = Door.GetComponent<Door>();
        exposedEnemies = 0;
    }

    private void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        totalEnemies = 1;
    }

    public static void EnemyExposed(GameObject enemy, Color color) {

        if (enemy.GetComponent<EnemyMovement>().isExposed) return;

        enemy.GetComponent<EnemyMovement>().isExposed = true;
        exposedEnemies++;
        
        // Add material
        MeshRenderer meshR = enemy.AddComponent<MeshRenderer>();
        meshR.material = instance.enemyMaterial;
        meshR.material.SetColor("_Color", color);

        if (exposedEnemies == totalEnemies) {
            instance.door.OpenDoor();
        }
    }
}
