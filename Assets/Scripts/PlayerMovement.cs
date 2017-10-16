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

    private Vector3 m_rotation;
    private Vector3 m_cameraRotation;
    private float m_lookSensitivity = 1.5f;

    private Vector3 m_moveHorizontal;
    private Vector3 m_moveForward;

    private float m_verticalVelocity;
    private bool isOnFloor = false;

    private Rigidbody m_Rigid;

    private GameObject shadow;
    private RaycastHit raycastHit;

    private static PlayerMovement instance;

    [Header("The Camera the player looks through")]
    public Camera m_Camera;
    public GameObject shadowPrefab;

    // Use this for initialization
    private void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        m_Rigid = GetComponent<Rigidbody>();
        shadow = Instantiate(shadowPrefab, transform.position + transform.up * -1 * transform.localScale.y, transform.rotation);
        shadow.transform.localScale = new Vector3(3, 0, 3);
    }

    public static void UpdateCamera(Vector2 updatedLocation) {
        instance.m_rotation = new Vector3(0, updatedLocation.x, 0) * instance.m_lookSensitivity;
        instance.m_cameraRotation = new Vector3(updatedLocation.y, 0, 0) * instance.m_lookSensitivity;
    }

    public static void UpdatePlayerLocation(Vector2 updatedLocation) {
        instance.m_moveHorizontal = instance.transform.right * updatedLocation.x;
        instance.m_moveForward = instance.transform.forward * updatedLocation.y;
    }

    public static void UpdatePlayerJump(bool TriggerJump) {
        if (instance.isOnFloor && TriggerJump)
            instance.m_verticalVelocity = 14;
        else
            instance.m_verticalVelocity = instance.m_Rigid.velocity.y;
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

        // Velocity of platform
        var otherVelocity = transform.parent != null ? transform.parent.GetComponentInParent<Rigidbody>().velocity : Vector3.zero;
        m_Rigid.velocity = (m_moveHorizontal + m_moveForward).normalized * speed + m_verticalVelocity * Vector3.up + otherVelocity;

        // Update player shadow
        if (Physics.Raycast(transform.position, transform.up * -1, out raycastHit, 30.0f)) {
            shadow.transform.position = raycastHit.point + raycastHit.normal * 0.01f;
            shadow.transform.rotation = transform.rotation;
        }
    }

    private void OnCollisionStay(Collision collision) {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Floor") && !collision.gameObject.CompareTag("Platform")) return;
        isOnFloor = true;
    }

    private void OnCollisionExit(Collision collision) {
        isOnFloor = false;
    }
}