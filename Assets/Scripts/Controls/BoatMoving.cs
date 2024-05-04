using System.Collections;
using UnityEngine;

public class BoatMoving : MonoBehaviour
{
    // Blades
    [SerializeField] GameObject leftBlade;
    [SerializeField] GameObject rightBlade;
    // Fasterings
    [SerializeField] GameObject leftFastering;
    [SerializeField] GameObject rightFastering;
    // Blades zero positions to count relatively
    [SerializeField] GameObject leftBladeZero;
    [SerializeField] GameObject rightBladeZero;

    // Actual speed
    [SerializeField] float speed = 0;
    // Boat speed multiplier
    [SerializeField] float boatSpeed = 9;
    // Boat drag multiplier
    [SerializeField] float drag = 0.4f;

    Rigidbody body;

    // Blades speed
    Vector3 leftBladeSpeed;
    Vector3 rightBladeSpeed;

    // Blades zero position speed
    Vector3 leftBladeZeroSpeed;
    Vector3 rightBladeZeroSpeed;

    // Last positioons of blades
    Vector3 leftBladeLastPosition;
    Vector3 rightBladeLastPosition;

    // Last positions of blades zero positions
    Vector3 leftBladeZeroLastPosition;
    Vector3 rightBladeZeroLastPosition;

    // Boat Y position (to fix the bug)
    private const float boatMinY = 7.025f;

    void Start()
    {
        // Initialize
        body = GetComponent<Rigidbody>();

        leftBladeLastPosition = leftBlade.transform.position;
        rightBladeLastPosition = rightBlade.transform.position;

        leftBladeZeroLastPosition = leftBladeZero.transform.position;
        rightBladeZeroLastPosition = rightBladeZero.transform.position;

        // Bug fix
        StartCoroutine(FixPosition());
    }

    // Bug fix, TODO: find another way to fix bug
    IEnumerator FixPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            // Prevent trembling
            transform.position = new Vector3(transform.position.x, boatMinY, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        // Initialize
        Vector3 leftSpeed = Vector3.zero;
        Vector3 rightSpeed = Vector3.zero;

        Vector3 leftBladeLocalPosition = leftBlade.transform.position;
        Vector3 rightBladeLocalPosition = rightBlade.transform.position;

        Vector3 leftBladeZeroLocalPosition = leftBladeZero.transform.position;
        Vector3 rightBladeZeroLocalPosition = rightBladeZero.transform.position;

        // Blades speed
        leftBladeSpeed = new Vector3(leftBladeLocalPosition.x - leftBladeLastPosition.x, 0, leftBladeLocalPosition.z - leftBladeLastPosition.z) / Time.deltaTime;
        rightBladeSpeed = new Vector3(rightBladeLocalPosition.x - rightBladeLastPosition.x, 0, rightBladeLocalPosition.z - rightBladeLastPosition.z) / Time.deltaTime;

        // Blades zero positions speed
        leftBladeZeroSpeed = new Vector3(leftBladeZeroLocalPosition.x - leftBladeZeroLastPosition.x, 0, leftBladeZeroLocalPosition.z - leftBladeZeroLastPosition.z) / Time.deltaTime;
        rightBladeZeroSpeed = new Vector3(rightBladeZeroLocalPosition.x - rightBladeZeroLastPosition.x, 0, rightBladeZeroLocalPosition.z - rightBladeZeroLastPosition.z) / Time.deltaTime;

        // Count blades speed relatively to blades zero positions speed
        leftBladeSpeed -= leftBladeZeroSpeed;
        rightBladeSpeed -= rightBladeZeroSpeed;

        // Reinit last positions
        leftBladeLastPosition = leftBlade.transform.position;
        rightBladeLastPosition = rightBlade.transform.position;

        leftBladeZeroLastPosition = leftBladeZero.transform.position;
        rightBladeZeroLastPosition = rightBladeZero.transform.position;

        // If blade is underwater (under boat bottom)
        if (leftBlade.transform.position.y < transform.position.y)
        {
            // Read oar velocity
            leftSpeed = leftBladeSpeed;
            // Add resistance of the oar uderwater
            leftSpeed += body.velocity * drag;
        }

        // If blade is underwater (under boat bottom)
        if (rightBlade.transform.position.y < transform.position.y)
        {
            // Read oar velocity
            rightSpeed = rightBladeSpeed;
            // Add resistance of the oar uderwater
            rightSpeed += body.velocity * drag;
        }

        // Moving forward is faster than moving backward, so add drag
        Vector3 localVelocity = transform.InverseTransformDirection(body.velocity);
        speed = -localVelocity.x;
        float forwardSpeed = boatSpeed;
        if (speed < 0)
        {
            forwardSpeed += forwardSpeed * speed;
        }

        // Add speed to the boat
        body.AddForceAtPosition(-leftSpeed * forwardSpeed, leftFastering.transform.position, ForceMode.Force);
        body.AddForceAtPosition(-rightSpeed * forwardSpeed, rightFastering.transform.position, ForceMode.Force);
    }
}
