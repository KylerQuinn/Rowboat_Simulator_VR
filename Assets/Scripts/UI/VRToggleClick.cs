using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRToggleClick : MonoBehaviour
{
    public void OnInteract()
    {
        Toggle toggle = GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.isOn = !toggle.isOn;
        }
    }
}
