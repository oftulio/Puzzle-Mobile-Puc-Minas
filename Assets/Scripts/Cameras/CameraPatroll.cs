using UnityEngine;

public class CameraPatrol : MonoBehaviour
{
    public float angleMin = -45f;
    public float angleMax = 45f;
    public float speed = 30f;

    public float currentAngle;
    public int direction = 1;
    public float originalXRotation; // salva a inclinação original

    void Start()
    {
        originalXRotation = transform.localEulerAngles.x;
        currentAngle = transform.localEulerAngles.y;
    }

    void Update()
    {
        currentAngle += direction * speed * Time.deltaTime;

        if (currentAngle >= angleMax)
        {
            currentAngle = angleMax;
            direction = -1;
        }
        else if (currentAngle <= angleMin)
        {
            currentAngle = angleMin;
            direction = 1;
        }

        transform.localRotation = Quaternion.Euler(originalXRotation, currentAngle, 0);
    }
}
