using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Implementation class for buttons to check if button still pressed

    bool buttonPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 

    }

    public bool ButtonPressed()
    { 
        return buttonPressed; 
    }
}
