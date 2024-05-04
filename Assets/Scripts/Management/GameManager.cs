using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject missionCompleteText;
    [SerializeField] GameObject secretFoundText;

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject winCamera;

    bool missionComplete;
    bool secretFound;

    const float cameraSwitchDelay = 4f;
    
    void Start()
    {
        // Init
        missionComplete = false;
        secretFound = false;
    }

    public bool IsMissionComplete()
    {
        // Check if mission completed
        return missionComplete;
    }
    public bool IsSecretFound()
    {
        // Check if secret found
        return secretFound;
    }

    public void CompleteMission()
    {
        // When boat hits "win" gameobject
        missionComplete = true;
        missionCompleteText.SetActive(true);
        StartCoroutine(SwitchCamera());
    }

    public void FoundSecret()
    {
        // When boat hits "secret" gameobject
        secretFound = true;
        secretFoundText.SetActive(true);
    }

    IEnumerator SwitchCamera()
    {
        // Switch cameras when boat hits "win" gameobject
        mainCamera.SetActive(false);
        winCamera.SetActive(true);

        yield return new WaitForSeconds(cameraSwitchDelay);

        winCamera.SetActive(false);
        mainCamera.SetActive(true);
    }
}
