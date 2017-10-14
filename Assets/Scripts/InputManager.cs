using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    private void Start() {
        StartCoroutine("WaitForInput");
    }

    IEnumerator WaitForInput() {
        while(true) {
            if (GamepadInput.GamePad.GetTrigger(GamepadInput.GamePad.Trigger.RightTrigger, false) == 1 ||
                GamepadInput.GamePad.GetTrigger(GamepadInput.GamePad.Trigger.LeftTrigger, false) == 1) {
                PaintGun.instance.ShootBullet();
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

    }
}
