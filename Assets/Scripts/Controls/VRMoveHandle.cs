using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMoveHandle : MonoBehaviour
{
    [SerializeField] List<GameObject> handles;
    [SerializeField] List<MeshRenderer> ArrowRenderers;
    [SerializeField] Material OriginalMaterial;
    [SerializeField] Material InteractiveMaterial;
    [SerializeField] bool isForward;

    public void OnPointerEnter()
    {
        foreach (GameObject handle in handles)
        {
            if (isForward)
            {
                handle.SendMessage("VRMoveForward");
            }
            else
            {
                handle.SendMessage("VRMoveBack");
            }
        }

        foreach (MeshRenderer renderer in ArrowRenderers)
        {
            renderer.material = InteractiveMaterial;
        }
    }

    public void OnPointerExit()
    {
        foreach (GameObject handle in handles)
        {
            handle.SendMessage("VRMoveStop");
        }

        foreach (MeshRenderer renderer in ArrowRenderers)
        {
            renderer.material = OriginalMaterial;
        }
    }
}
