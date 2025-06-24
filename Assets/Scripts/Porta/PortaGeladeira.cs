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
    public bool coletouchave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //chaveColetavel = GetComponent<ChaveColetavel>();
        //portaGeladeira = GetComponent<PortaGeladeira>();
        targetRotation = doorPivot.rotation;
        coletouchave = chaveColetavel.ColetouChave;
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
        if (other.CompareTag("Player"))
        {
            RefButtonInteragir.SetActive(false);
            canvasPortaGeladeira.SetActive(false);
        }
            
    }

    public void IsOpen()
    {
        
        
            //float angle = rotationAngle;
            targetRotation = Quaternion.Euler(0, -90, 0); // Gira no eixo Y
            //portaGeladeira.GetComponent<PortaGeladeira>().enabled = false;
            RefButtonInteragir.SetActive(false);
            canvasPortaGeladeira.SetActive(false);
        
       
           // UIManager.Instance.MostrarMensagem("Não tem a chave da cozinha");

    }


}
