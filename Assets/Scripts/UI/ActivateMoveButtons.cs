using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMoveButtons : MonoBehaviour
{
    [SerializeField] GameObject buttonsGroup;
    [SerializeField] GameObject anotherButtonsGroup;
    [SerializeField] GameObject bothButtonsGroup;

    public void OnInteract()
    {
        buttonsGroup.SetActive(!buttonsGroup.activeSelf);
        bothButtonsGroup.SetActive(buttonsGroup.activeSelf && anotherButtonsGroup.activeSelf);
    }
}
