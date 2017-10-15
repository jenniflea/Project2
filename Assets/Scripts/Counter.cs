using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {

    public int totalEnemies;
    public int totalPlatforms;
    public GameObject Door;
    public static Counter instance;
    public Material material;

    public static bool AllEnemiesExposed {
        get {
            return exposedEnemies >= instance.totalEnemies;
        }
    }

    public static bool AllPlatformsExposed {
        get {
            return exposedPlatforms >= instance.totalPlatforms;
        }
    }

    public static bool DoorIsOpen {
        get {
            return AllPlatformsExposed && AllEnemiesExposed;
        }
    }

    private static int exposedEnemies;
    private static int exposedPlatforms;
    private Door door;

    private void Awake() {
        door = Door.GetComponent<Door>();
        exposedEnemies = 0;
        exposedPlatforms = 0;
    }

    private void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        totalEnemies = 4;
        totalPlatforms = 3;
    }

    public static void EnemyExposed(GameObject enemy, Color color) {
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement.isExposed) return;

        exposedEnemies++;
        enemyMovement.isExposed = true;
        AddMaterialToObject(enemy, color);
        OpenDoorIfNeeded();
    }

    public static void PlatformExposed(GameObject platformObj, Color color) {
        Platform platform = platformObj.GetComponent<Platform>();
        if (platform.isExposed) return;

        exposedPlatforms++;
        platform.isExposed = true;
        AddMaterialToObject(platformObj, color);
        OpenDoorIfNeeded();
    }

    private static void OpenDoorIfNeeded() {
        if (AllEnemiesExposed && AllPlatformsExposed)
            instance.door.OpenDoor();
    }

    private static void AddMaterialToObject(GameObject gameObj, Color color) {
        MeshRenderer meshR = gameObj.AddComponent<MeshRenderer>();
        meshR.material = instance.material;
        meshR.material.SetColor("_Color", color);
    }
}
