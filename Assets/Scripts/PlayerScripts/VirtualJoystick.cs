using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickKnob;
    public Vector2 inputDirection;

    private Vector2 joystickCenter;

    private void Start()
    {
        joystickCenter = joystickBackground.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickCenter;
        float radius = joystickBackground.sizeDelta.x / 2;
        inputDirection = Vector2.ClampMagnitude(direction / radius, 1.0f);
        joystickKnob.position = joystickCenter + (inputDirection * radius);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickKnob.position = joystickCenter;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
}
