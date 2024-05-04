using UnityEngine;

public class TurnBack : MonoBehaviour
{
    [SerializeField] UIButtonPressed turnLeftButton, turnRightButton;

    Animator cameraAnimator;

    // Keyboard or joystick input
    bool turnLeftInput = false;
    bool turnRightInput = false;

    // UI input
    bool turnRightUI = false;
    bool turnLeftUI = false;

    void Start()
    {
        // Read camera animator component
        cameraAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check UI buttons press if they exist 
        if (turnLeftButton != null)
        {
            turnLeftUI = turnLeftButton.ButtonPressed();
        }
        if (turnRightButton != null)
        {
            turnRightUI = turnRightButton.ButtonPressed();
        }

        // Read keyboard or joystick input
        float horizontal = Input.GetAxis("Head Rotation");

        // Check keyboard of joystick buttons press
        if (!Mathf.Approximately(horizontal, 0))
        {
            if (horizontal < 0)
            {
                turnLeftInput = true;
            }
            else
            {
                turnRightInput = true;
            }
        }
        else
        {
            turnLeftInput = false;
            turnRightInput = false;
        }

        // Setting animator triggers
        cameraAnimator.SetBool("TurnLeft", turnLeftInput || turnLeftUI);
        cameraAnimator.SetBool("TurnRight", turnRightInput || turnRightUI);
    }
}
