using System.Collections;
using UnityEngine;

public class MissionText : MonoBehaviour
{
    [SerializeField] float animationDuration = 3f;

    void OnEnable()
    {
        // Show animation on gameobject enable
        StartCoroutine(WaitTillTheAnimationEnds());
    }

    IEnumerator WaitTillTheAnimationEnds()
    {
        // Wait
        yield return new WaitForSeconds(animationDuration);

        // Disable gameobject
        gameObject.SetActive(false);
    }
}
