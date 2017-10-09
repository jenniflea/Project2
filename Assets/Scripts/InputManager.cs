using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public PaintGun paintGun;

	void Update () {
        if (GamepadInput.GamePad.GetTrigger(GamepadInput.GamePad.Trigger.RightTrigger, false) == 1)
            paintGun.ShootBullet();
	}
}
