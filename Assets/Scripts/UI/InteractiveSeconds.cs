using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSeconds : MonoBehaviour
{
    [SerializeField] int seconds = 3;

    public int GetSeconds()
    {
        return seconds;
    }
}
