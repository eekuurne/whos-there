using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] Transform playerCamera;
    [SerializeField] float XSensitivity = 1f;
    [SerializeField] float YSensitivity = 1f;
    [SerializeField] bool clampVerticalRotation = true;
    [SerializeField] float MinimumX = -90F;
    [SerializeField] float MaximumX = 90F;
    [SerializeField] float smoothTime = 5f;
    [SerializeField] bool lockCursor = true;

    Quaternion m_CharacterTargetRot;
    Quaternion m_CameraTargetRot;
    bool m_cursorIsLocked = true;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        m_CharacterTargetRot = transform.localRotation;
        m_CameraTargetRot = playerCamera.localRotation;
        m_cursorIsLocked = lockCursor;
        InternalLockUpdate();
    }

    void Update() {
        LookRotation();
    }

    void FixedUpdate() {
        Vector3 move = MovementVector();

        // Normalized movement amount
        if (move.magnitude > 1) {
            move = move.normalized;
        }
        move.x *= movementSpeed;
        move.z *= movementSpeed;
        move = transform.TransformDirection(move);

        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    Vector3 MovementVector() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        return new Vector3(h, 0, v);
    }

    void LookRotation() {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        transform.rotation = Quaternion.Slerp(transform.localRotation, m_CharacterTargetRot, smoothTime);
        playerCamera.localRotation = Quaternion.Slerp(playerCamera.localRotation, m_CameraTargetRot, smoothTime);
    }

    public void HideCursor(bool value) {
        m_cursorIsLocked = value;
        InternalLockUpdate();
    }

    private void InternalLockUpdate() {
        if (m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else if (!m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q) {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
