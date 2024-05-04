using UnityEngine;

public class RightHandle : MonoBehaviour
{
    [SerializeField] GameObject boat;
    [SerializeField] GameObject gravityPoint;
    [SerializeField] GameObject handle;

    [SerializeField] FloatingJoystick joystick;

    [SerializeField] float oarSpeed = 100;
    [SerializeField] float gravityForce = 2;

    [SerializeField] int minX = -12;
    [SerializeField] int maxX = 25;
    [SerializeField] int maxY = 50;

    Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Add gravity to oar to put it down under water
        body.AddForceAtPosition(Vector3.down * gravityForce, gravityPoint.transform.position);

        // Keyboard Input
        float keyboardHorizontal = Input.GetAxis("Horizontal Right");
        float keyboardVertical = Input.GetAxis("Vertical Right");

        // Touchscreen input
        float joystickHoriaontal = joystick.Horizontal;
        float joystickVertical = joystick.Vertical;

        // Keyboard and gamepad has priority
        float horizontal = keyboardHorizontal;
        if (Mathf.Approximately(keyboardHorizontal, 0))
        {
            horizontal = joystickHoriaontal;
        }
        float vertical = keyboardVertical;
        if (Mathf.Approximately(keyboardVertical, 0))
        {
            vertical = joystickVertical;
        }

        // Local angles between -180 and 180
        float x = Mathf.Abs(body.transform.localEulerAngles.x) > 180 ? body.transform.localEulerAngles.x % 360 - 360 : body.transform.localEulerAngles.x;
        float y = Mathf.Abs(body.transform.localEulerAngles.y) > 180 ? body.transform.localEulerAngles.y % 360 - 360 : body.transform.localEulerAngles.y;

        // Rotation given by input
        x += vertical * oarSpeed * Time.deltaTime;
        y += horizontal * oarSpeed * Time.deltaTime;

        // Limit x and y with max a min values
        x = Mathf.Clamp(x, minX, maxX);
        y = Mathf.Clamp(y, -maxY, maxY);

        // Read boat rotation in world coordinates
        Vector3 boatRotation = boat.transform.rotation.eulerAngles;

        // Prevent z-axis rotation
        Quaternion bodyZeroZRotation = Quaternion.Euler(new Vector3(boatRotation.x + x, boatRotation.y + y, boatRotation.z));

        // Add rotation to the rigidbody
        body.MoveRotation(bodyZeroZRotation);
    }

    // Rotation based on forces (experimental function, not in use)
    private void AFixedUpdate()
    {
        // Add gravity to oar to put it down underwater
        body.AddForceAtPosition(Vector3.down * gravityForce, gravityPoint.transform.position);

        // Keyboard Input
        float horizontal = Input.GetAxis("Horizontal Right");
        float vertical = Input.GetAxis("Vertical Right");

        // Read boat rotation in world coordinates
        Vector3 boatRotation = boat.transform.rotation.eulerAngles;

        // Rotation of the oar
        float x = handle.transform.rotation.eulerAngles.x;
        float y = handle.transform.rotation.eulerAngles.y;

        // Vectors of forces
        Vector3 xForce = vertical * oarSpeed * new Vector3(0, Mathf.Cos(x * Mathf.Deg2Rad), Mathf.Sin(x * Mathf.Deg2Rad));
        Vector3 yForce = horizontal * oarSpeed * new Vector3(Mathf.Cos(y * Mathf.Deg2Rad), 0, -Mathf.Sin(y * Mathf.Deg2Rad));

        // Position of the oar's handle
        Vector3 position = handle.transform.position;

        // Add force to the handle
        body.AddForceAtPosition(yForce, position);
        body.AddForceAtPosition(xForce, position);

        Vector3 bodyRotation = body.rotation.eulerAngles;

        float localX = bodyRotation.x;
        float localY = bodyRotation.y;

        // Prevent z-axis rotation
        Quaternion bodyZeroZRotation = Quaternion.Euler(new Vector3(localX, localY, boatRotation.z));
        body.MoveRotation(bodyZeroZRotation);
    }

}
