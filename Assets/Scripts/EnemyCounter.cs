using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {

    public static int totalEnemies;
    public GameObject Door;
    public static EnemyCounter instance;

    private static int exposedEnemies = 0;
    private Door door;

    private void Awake() {
        door = Door.GetComponent<Door>();
    }

    private void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        totalEnemies = 1;
    }

    public static void EnemyExposed() {
        exposedEnemies++;

        if (exposedEnemies == totalEnemies) {
            instance.door.OpenDoor();
        }
    }
}
