using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRButtonPress : MonoBehaviour
{
    public void OnInteract()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.Invoke();
        }
    }
}
