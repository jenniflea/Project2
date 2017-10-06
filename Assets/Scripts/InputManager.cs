using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public PaintGun paintGun;

	void Update () {
        if (GamepadInput.GamePad.GetTrigger(GamepadInput.GamePad.Trigger.RightTrigger, GamepadInput.GamePad.Index.Any, false) == 0)
            return;

        paintGun.ShootBullet();
		
	}
}
