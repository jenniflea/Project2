using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    public int totalEnemies = 4;
    public int totalPlatforms = 4;
    public GameObject Door;
    public Text ItemsSplat;
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

        UpdateSplatItemsText();
    }

    public static void EnemyExposed(GameObject enemy, Color color) {
        EnemyMovement enemyMovement = enemy.GetComponentInParent<EnemyMovement>();
        if (enemy.GetComponentInParent<AudioSource>() == null) Debug.Log(enemy.transform.parent.name);
        enemy.GetComponentInParent<AudioSource>().Play();
        if (enemyMovement.isExposed) return;

        exposedEnemies++;
        enemyMovement.isExposed = true;
        AddMaterialToObject(enemy.transform.parent.gameObject, color);
        instance.UpdateSplatItemsText();
        OpenDoorIfNeeded();
    }

    public static void PlatformExposed(GameObject platformObj, Color color) {
        Platform platform = platformObj.GetComponent<Platform>();
        platformObj.GetComponent<AudioSource>().Play();
        if (platform.isExposed) return;

        exposedPlatforms++;
        platform.isExposed = true;
        platformObj.layer = LayerMask.NameToLayer("Default");
        AddMaterialToObject(platformObj, color);
        instance.UpdateSplatItemsText();
        OpenDoorIfNeeded();
    }

    private void UpdateSplatItemsText() {
        ItemsSplat.text = (exposedEnemies + exposedPlatforms).ToString() + "/" + (totalEnemies + totalPlatforms).ToString();
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
