using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColisionGeneral : MonoBehaviour
{
    public DialogoBaronesa dialogoBaronesa;
    public GameObject Baronesa;
    public AudioSource MatouBarao;
    public AudioClip SomMatouBarao;
    public GameObject BilharButton;
    public GameObject PanfletoPainel1;
    public GameObject PanfletoPainel2;
    public GameObject PanfletoPainel3;
    public GameObject PanfletoDica1;
    public GameObject PanfletoSemChave;
    public GameObject InteragirGeladeiraButton;
    public GameObject InteragirButton;
    public GameObject InteragirButtonDica;
    public GameObject InteragirPortaButton;
    public GameObject fecharPanfleto;
    public GameObject fecharPuzzleGeladeiraButton;
    private MobileLook playerScript;
    public GameObject player; // ReferÍncia ao player
    private ChaveColetavel chaveScript;
    public bool TemAChave = false;

    public GameObject RefButtonInteragir;
    public GameObject canvasPortaGeladeira;


    public Transform doorPivot;
    public Transform GeladeiraPivot;
    public float rotationAngle = -90f;
    public float rotationAngleGela = -90f;
    public float rotationSpeed = 2f;
    private bool isOpen = false;
    private bool GeladeiraisOpen = false;
    private Quaternion targetRotation;
    private Quaternion targetRotationGeladeira;
    public bool portaAbriu = false;

    public GameManagerSinuca gamemanagerSinuca;
    public GameObject GameManagerSinuca;

    public GameObject BanheiraButton;

    public GameObject Barao;
    public string nextSceneName = "FimDoJogo";

    public GameObject MatarBaraoButton;
 

    void Start()
    {
        dialogoBaronesa = Baronesa.GetComponent<DialogoBaronesa>();
        playerScript = player.GetComponent<MobileLook>();
        gamemanagerSinuca = GameManagerSinuca.GetComponent<GameManagerSinuca>();
        targetRotation = doorPivot.rotation;
        targetRotationGeladeira = GeladeiraPivot.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        doorPivot.rotation = Quaternion.Slerp(doorPivot.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        GeladeiraPivot.rotation = Quaternion.Slerp(GeladeiraPivot.rotation, targetRotationGeladeira, Time.deltaTime * rotationSpeed);
        if (portaAbriu == true)
        {
            InteragirPortaButton.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Panfleto1"))
            InteragirButton.SetActive(true);
        
        if (other.CompareTag("Panfleto2"))
            InteragirButton.SetActive(true);
        
        if (other.CompareTag("Panfleto3"))
            InteragirButton.SetActive(true);
        if (other.CompareTag("PanfletoDica1"))
            InteragirButtonDica.SetActive(true);

        if (other.CompareTag("Porta"))
        {
            InteragirPortaButton.SetActive(true);
        }
        if (other.CompareTag("Geladeira"))
        {
            InteragirGeladeiraButton.SetActive(true);
        }
        if (other.CompareTag("PortaCozinha"))
        {
            RefButtonInteragir.SetActive(true);
            canvasPortaGeladeira.SetActive(true);
        }
        if (other.CompareTag("Bilhar") && dialogoBaronesa.TerminouDialogoBilharvolta == true)
            BilharButton.SetActive(true);

        if (other.CompareTag("Banheira"))
            BanheiraButton.SetActive(true);

        if (other.CompareTag("Barao"))
            MatarBaraoButton.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        InteragirButton.SetActive(false);
        InteragirPortaButton.SetActive(false);
        PanfletoPainel1.SetActive(false);
        PanfletoPainel2.SetActive(false);
        PanfletoPainel3.SetActive(false);
        PanfletoDica1.SetActive(false);
        PanfletoSemChave.SetActive(false);
        InteragirGeladeiraButton.SetActive(false);
        RefButtonInteragir.SetActive(false);
        canvasPortaGeladeira.SetActive(false);
        BilharButton.SetActive(false);
        BanheiraButton.SetActive(false);
        MatarBaraoButton.SetActive(false);
        InteragirButtonDica.SetActive(false);
    }

    public void InteragirPanfleto1()
    {

        PanfletoPainel1.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }
    public void InteragirPanfleto2()
    {
        PanfletoPainel2.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }
    public void InteragirPanfleto3()
    {
        PanfletoPainel3.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }
    public void InteragirPanfletoDica()
    {
        PanfletoDica1.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }
    public void InteragirPorta()
    {
        if (TemAChave == true)
        {
            isOpen = !isOpen;
            float angle = isOpen ? rotationAngle : 0f;
            targetRotation = Quaternion.Euler(0, angle, 0); // Gira no eixo Y
            InteragirPortaButton.SetActive(false);
            portaAbriu = true;
        }
        else
        {
            PanfletoSemChave.SetActive(true);
        }
    }

    public void FecharPanfleto()
    {
        PanfletoPainel1.SetActive(false);
        PanfletoPainel2.SetActive(false);
        PanfletoPainel3.SetActive(false);
        PanfletoDica1.SetActive(false);
        playerScript.moveCamera = true;
        playerScript.movePlayer = true;
    }

    public void AbrirPuzzle()
    {
        fecharPuzzleGeladeiraButton.SetActive(true);
        GeladeiraisOpen = !GeladeiraisOpen;
        InteragirGeladeiraButton.SetActive(false);
        float angle = GeladeiraisOpen ? rotationAngle : 0f;
        targetRotationGeladeira = Quaternion.Euler(0, angle, 0);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
    }

    public void FecharPuzzle()
    {

        playerScript.moveCamera = true;
        playerScript.movePlayer = true;
        fecharPuzzleGeladeiraButton.SetActive(false);
        targetRotationGeladeira = Quaternion.Euler(0, 0, 0);
        GeladeiraisOpen = false;
    }

    public void MatarBarao()
    {
        MatouBarao.PlayOneShot(SomMatouBarao);
        Destroy(Barao);
        SceneManager.LoadScene(nextSceneName);

    }
}
