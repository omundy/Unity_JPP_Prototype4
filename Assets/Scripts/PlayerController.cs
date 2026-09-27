using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions controls;
    public float playerSpeed = 500f;
    public Rigidbody playerRb;
    GameObject focalPoint;
    public bool hasPowerup;
    float powerupStrength = 15f;
    public GameObject powerupIndicator;

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
        playerRb.AddForce(
            focalPoint.transform.forward * forwardInput * playerSpeed * Time.deltaTime
        );

        powerupIndicator.transform.position = transform.position + new Vector3(0, -.5f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            Debug.Log(
                "Collided with " + collision.gameObject.name + " while PowerUp set to " + hasPowerup
            );
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
