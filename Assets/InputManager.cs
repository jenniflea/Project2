using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public PaintGun paintGun;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            paintGun.ShootBullet();
        }
		
	}
}
