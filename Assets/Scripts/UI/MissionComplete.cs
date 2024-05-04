using UnityEngine;

public class MissionComplete : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Init game manager
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if game manager exists
        if (gameManager != null)
        {
            // Switch cameras and show text when boat hits "win" gameobject
            if (other.CompareTag("Finish"))
            {
                if (!gameManager.IsMissionComplete())
                {
                    gameManager.CompleteMission();
                }
            }

            // Show text when boat hits "secret" gameobject
            if (other.CompareTag("Secret"))
            {
                if (!gameManager.IsSecretFound())
                {
                    gameManager.FoundSecret();
                }
            }
        }
    }
}
