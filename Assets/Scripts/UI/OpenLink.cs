using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [SerializeField] string url;

    public void OpenURL()
    {
        // Open given link
        Application.OpenURL(url);
    }
}
