using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PortaInternaSalao : MonoBehaviour
{
    public Transform doorPivot;
    private Quaternion targetRotation;
    public float rotationAngle = 90;
    public float rotationSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRotation = doorPivot.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        doorPivot.rotation = Quaternion.Slerp(doorPivot.rotation, targetRotation, Time.deltaTime * rotationSpeed);
       
           
        
    }

    public void IsOpen()
    {
        float angle = rotationAngle;
        targetRotation = Quaternion.Euler(0, angle, 0); // Gira no eixo Y
    }

   
}
