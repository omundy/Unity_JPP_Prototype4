using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions controls;
    public float playerSpeed = 500f;
    public Rigidbody playerRb;
    GameObject focalPoint;

    void Awake()
    {
        controls = new InputSystem_Actions();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    void OnEnable()
    {
        controls.Player.Enable();
        // Debug.Log(controls.Player.Move);
    }

    void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float forwardInput = moveInput.y; 
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * playerSpeed * Time.deltaTime);
    }
}
