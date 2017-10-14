// FPS Controller
// 1. Create a Parent Object like a 3D model
// 2. Make the Camera the user is going to use as a child and move it to the height you wish. 
// 3. Attach a Rigidbody to the parent
// 4. Drag the Camera into the m_Camera public variable slot in the inspector
// Escape Key: Escapes the mouse lock
// Mouse click after pressing escape will lock the mouse again


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private float speed = 10.0f;
    private float m_MovX;
    private float m_MovY;
    private Vector3 m_moveHorizontal;
    private Vector3 m_movForward;
    private float m_verticalVelocity;
    private Vector3 m_velocity;
    private Rigidbody m_Rigid;
    private float m_yRot;
    private float m_xRot;
    private Vector3 m_rotation;
    private Vector3 m_cameraRotation;
    private float m_lookSensitivity = 3.0f;
    private bool isOnFloor = false;

    [Header("The Camera the player looks through")]
    public Camera m_Camera;

    // Use this for initialization
    private void Start() {
        m_Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update() {

        // camera movement
        var m_Rot = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.RightStick, false);
        m_xRot = m_Rot.x;
        m_rotation = new Vector3(0, m_xRot, 0) * m_lookSensitivity;

        m_yRot = m_Rot.y;
        m_cameraRotation = new Vector3(m_yRot, 0, 0) * m_lookSensitivity;

        // player movement
        var m_Mov = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, false);
        m_MovX = m_Mov.x;
        m_MovY = m_Mov.y;

        m_moveHorizontal = transform.right * m_MovX;
        m_movForward = transform.forward * m_MovY;
        if (isOnFloor && GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A))
            m_verticalVelocity = 1000;
        else
            m_verticalVelocity = 0;
   }

    private void FixedUpdate() {
        //rotate the camera of the player
        if (m_rotation != Vector3.zero) {
            m_Rigid.MoveRotation(m_Rigid.rotation * Quaternion.Euler(m_rotation));
        }

        if (m_Camera != null) {
            //negate this value so it rotates like a FPS not like a plane
            m_Camera.transform.Rotate(-m_cameraRotation);
        }

        var otherVelocity = transform.parent != null ? transform.parent.GetComponentInParent<Rigidbody>().velocity : Vector3.zero;
        var verticalVelocity = m_Rigid.velocity.y + m_verticalVelocity * Time.fixedDeltaTime;
        m_Rigid.velocity = (m_moveHorizontal + m_movForward).normalized * speed + verticalVelocity * Vector3.up + otherVelocity;

    }

    private void OnCollisionStay(Collision collision) {
        isOnFloor = true;
    }

    private void OnCollisionExit(Collision collision) {
        isOnFloor = false;
    }
}