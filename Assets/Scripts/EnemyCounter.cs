using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {

    private int totalEnemies;
    private int exposedEnemies;

    public EnemyCounter instance;

	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
	}
}
