using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

    private void Update() {
        if (GamepadInput.GamePad.GetTrigger(GamepadInput.GamePad.Trigger.RightTrigger, false) == 1 ||
            GamepadInput.GamePad.GetTrigger(GamepadInput.GamePad.Trigger.LeftTrigger, false) == 1)
            PaintGun.instance.ShootBullet();

        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        var m_Rot = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.RightStick, false);
        var m_Mov = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, false);

        PlayerMovement.UpdateCamera(m_Rot);
        PlayerMovement.UpdatePlayerLocation(m_Mov);
        PlayerMovement.UpdatePlayerJump(GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A));
    }
}
