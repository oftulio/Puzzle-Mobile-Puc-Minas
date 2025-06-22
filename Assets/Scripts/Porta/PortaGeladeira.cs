using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PortaGeladeira : MonoBehaviour
{
    public Transform doorPivot;
    private Quaternion targetRotation;
    public float rotationAngle = -90f;
    public float rotationSpeed = 2f;
    public GameObject RefButtonInteragir;
    public GameObject canvasPortaGeladeira;
    public ChaveColetavel chaveColetavel;
    public PortaGeladeira portaGeladeira;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chaveColetavel = GetComponent<ChaveColetavel>();
        portaGeladeira = GetComponent<PortaGeladeira>();
        targetRotation = doorPivot.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        doorPivot.rotation = Quaternion.Slerp(doorPivot.rotation, targetRotation, Time.deltaTime * rotationSpeed);



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RefButtonInteragir.SetActive(true);
            canvasPortaGeladeira.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        RefButtonInteragir.SetActive(false);
        canvasPortaGeladeira.SetActive(false);
    }

    public void IsOpen()
    {
        if (chaveColetavel.ColetouChave == true)
        {
            float angle = rotationAngle;
            targetRotation = Quaternion.Euler(0, angle, 0); // Gira no eixo Y
            portaGeladeira.GetComponent<PortaGeladeira>().enabled = false;
            RefButtonInteragir.SetActive(false);
            canvasPortaGeladeira.SetActive(false);
        }
        else
            UIManager.Instance.MostrarMensagem("Você coletou a chave da cozinha");

    }


}
