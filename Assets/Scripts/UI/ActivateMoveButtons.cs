using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMoveButtons : MonoBehaviour
{
    [SerializeField] GameObject buttonsGroup;
    [SerializeField] GameObject anotherButtonsGroup;
    [SerializeField] GameObject bothButtonsGroup;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnInteract()
    {
        audioSource.Play();

        buttonsGroup.SetActive(!buttonsGroup.activeSelf);
        bothButtonsGroup.SetActive(buttonsGroup.activeSelf && anotherButtonsGroup.activeSelf);
    }
}
