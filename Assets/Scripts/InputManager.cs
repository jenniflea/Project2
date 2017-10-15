using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            yield return null;
        }

    }
}
