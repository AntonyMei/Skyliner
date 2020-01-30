using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMoving : MonoBehaviour
{
    public float Boost = 3f;
    public AnimationCurve MouseSensitivityCurve = 
        new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), 
                           new Keyframe(1f, 2.5f, 0f, 0f));
    float pitch, yaw, roll;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
        roll = transform.eulerAngles.z;
    }
    private void Update()
    {
        //Translate
        Vector3 translation = new Vector3();
        if (Input.GetKey(KeyCode.W)) {
           translation = Vector3.forward;
        } else if (Input.GetKey(KeyCode.S)) {
            translation = Vector3.back;
        } else if (Input.GetKey(KeyCode.A)) {
            translation = Vector3.left;
        } else if (Input.GetKey(KeyCode.D)) {
            translation = Vector3.right;
        } else if (Input.GetKey(KeyCode.E)) {
            translation = Vector3.up;
        } else if (Input.GetKey(KeyCode.Q)) {
            translation = Vector3.down;
        }
        translation *= (Time.deltaTime * Boost);
        if (Input.GetKey(KeyCode.LeftShift)) { translation *= 10f; }
        transform.Translate(translation,Space.Self);
        //Rotate
        Vector2 mouse_movement = 
            new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        float mouse_sensitivity_factor =
            MouseSensitivityCurve.Evaluate(mouse_movement.magnitude);
        yaw += mouse_movement.x * mouse_sensitivity_factor;
        pitch += mouse_movement.y * mouse_sensitivity_factor;
        transform.eulerAngles = new Vector3(pitch, yaw, roll);
    }
}
