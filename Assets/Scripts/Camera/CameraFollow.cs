using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Stolen script:) Rotation added
    
    // The position that that camera will be following.
    public Transform target;
    // The speed with which the camera will be following.           
    public float smoothing = 15f;

    // The initial offset from the target.
    Vector3 offset;
    Quaternion rotationOffset;

    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - target.position;
        rotationOffset = transform.rotation * Quaternion.Inverse(target.rotation);
    }

    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on 
        // the offset from the target.
        Vector3 targetCamPos = target.position + offset;
        Quaternion targetCamRot = target.rotation * rotationOffset;

        // Smoothly interpolate between the camera's current 
        // position and rotation and it's target position and rotation.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetCamRot, smoothing * Time.deltaTime);
    }
}
