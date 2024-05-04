using System.Collections.Generic;
using UnityEngine;

public class OarParticles : MonoBehaviour
{
    [SerializeField] GameObject waterRings;
    [SerializeField] GameObject instantiatePoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> OarIn;
    [SerializeField] List<AudioClip> OarOut;

    private void OnTriggerEnter(Collider other)
    {
        // When the oar goes under water
        if (other != null && other.CompareTag("Water"))
        {
            InstantiateWaterRings(other);
            PlayOarIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the oar goes out of water
        if (other != null && other.CompareTag("Water"))
        {
            InstantiateWaterRings(other);
            PlayOarOut();
        }
    }

    private void InstantiateWaterRings(Collider collider)
    {
        // Create water rings in the position of blade
        Instantiate(waterRings, instantiatePoint.transform.position, waterRings.transform.rotation);
    }

    private void PlayOarIn()
    {
        // Play random splash sound
        int count = OarIn.Count;

        if (audioSource != null && count > 0)
        {
            audioSource.clip = OarIn[Random.Range(0, count)];
            audioSource.Play();
        }
    }

    private void PlayOarOut()
    {
        // Play random splash sound
        int count = OarOut.Count;

        if (audioSource != null && count > 0)
        {
            audioSource.clip = OarOut[Random.Range(0, count)];
            audioSource.Play();
        }
    }
}
