using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    InputSystem_Actions controls;
    public float rotationSpeed = 150f;

    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        // Debug.Log(controls.Player.Move);
    }

    void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float horizontalInput = moveInput.x; // left/right || AD || arrow keys
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
