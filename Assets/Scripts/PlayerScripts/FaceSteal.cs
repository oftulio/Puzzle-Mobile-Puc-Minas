using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaceSteal : MonoBehaviour
{

    public Image playerFaceUI; // Referência ao Image da UI que mostra o rosto do player
    private EnemyAI nearbyEnemy; // Referência ao inimigo mais próximo
    public GameObject RoubarRostoButton;
    public GameObject RoubarRostoBaronesa;
    public bool RoubouFaceJardineiro = false;
    public bool RoubouFaceMordomo = false;
    public bool PodeRoubarFace;
    public DialogoMordomo dialogoMordomo;
    public DialogoBaronesa dialogoBaronesa;
    public GameObject canvas;
    public GameObject canvasBaronesa;
    public bool TerminouDiologoMordomo;
    public GameObject birdsPrefab;
    public Transform headPositionMordomo; // Posição da cabeça do inimigo
    public Transform headPositionBaronesa; // Posição da cabeça do inimigo
    public Transform headPositionJardineiro; // Posição da cabeça do inimigo
    public InimigoTonto Mordomotonto;
    public InimigoTonto Baronesatonta;
    public GameObject Mordomo;
    public GameObject Baronesa;
    public GameObject Jardineiro;
    public GameObject MordomoScript;
    public GameObject BaronesaScript;
    public GameObject PlayerRef;
    public FaceSteal faceSteal;
    [Header("Configurações")]
    public float anguloDeVisao = 60f;
    public string nomeCenaGameOver = "GameOver";
    public string nomeCenaGameOverInterna = "GameOverInterno";
    public AudioSource audioSource;
    public AudioClip SomRouboDeFace;

    public GameObject Barreira1, Barreira2;

    public GameObject ConversarButton;


    private void Start()
    {
        faceSteal = PlayerRef.GetComponent<FaceSteal>();
        dialogoMordomo = canvas.GetComponent<DialogoMordomo>();
       
        Mordomotonto = Mordomo.GetComponent<InimigoTonto>();
        Baronesatonta = Baronesa.GetComponent<InimigoTonto>();
        TerminouDiologoMordomo = dialogoMordomo.DialogoTerminou;
    }
    private void Update()
    {
        if (TerminouDiologoMordomo == true)
            dialogoMordomo.GetComponent<DialogoMordomo>().enabled = false;
    }



    void OnTriggerEnter(Collider other)
    {
        if (TerminouDiologoMordomo == true && RoubouFaceMordomo == false)
        {
            if (other.CompareTag("Mordomo"))
            {
                nearbyEnemy = other.GetComponent<EnemyAI>();
                RoubarRostoButton.SetActive(true);
            }
        }

        if (other.CompareTag("Jardineiro") && RoubouFaceJardineiro == false)
        {
            nearbyEnemy = other.GetComponent<EnemyAI>();
            RoubarRostoButton.SetActive(true);
        }
        if (other.CompareTag("Baronesa"))
        {
            if (RoubouFaceMordomo && dialogoBaronesa.PodeRoubarFace == true)
            {
                nearbyEnemy = other.GetComponent<EnemyAI>();
                RoubarRostoBaronesa.SetActive(true);
            }
            
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mordomo"))
        {
            nearbyEnemy = null;
            RoubarRostoButton.SetActive(false);
        }


        if (other.CompareTag("Jardineiro"))
        {
            nearbyEnemy = other.GetComponent<EnemyAI>();
            RoubarRostoButton.SetActive(false);
        }

        if (other.CompareTag("Baronesa"))
        {
            nearbyEnemy = other.GetComponent<EnemyAI>();
            RoubarRostoBaronesa.SetActive(false);
        }
    }

    public void StealFaceJardineiro()
    {


        if (nearbyEnemy == null) return;

        Transform inimigoTransform = nearbyEnemy.transform;

        Vector3 direcaoDoInimigo = inimigoTransform.forward;
        Vector3 direcaoParaPlayer = (transform.position - inimigoTransform.position).normalized;

        float angulo = Vector3.Angle(direcaoDoInimigo, direcaoParaPlayer);

        if (angulo < anguloDeVisao)
        {
            // Está na frente → Game Over
            SceneManager.LoadScene(nomeCenaGameOver);
        }

        else
        {
            audioSource.PlayOneShot(SomRouboDeFace);
            RoubouFaceJardineiro = true;
            playerFaceUI.sprite = nearbyEnemy.faceTexture; // Atualiza o rosto do player
            //Destroy(nearbyEnemy.gameObject); // Remove o inimigo
            //nearbyEnemy = null;
            RoubarRostoButton.SetActive(false);
            PlayerFaceManager.Instance.currentFace = "Jardineiro";
            Instantiate(birdsPrefab, headPositionJardineiro.position, Quaternion.identity, headPositionJardineiro);
            Mordomotonto.AtivarTontura();
            RoubarRostoButton.SetActive(false);
            
            MordomoScript.GetComponent<RandomPatrol>().enabled = false;
            PlayerRef.GetComponent<FaceSteal>().enabled = false;
            
        }


    }
    public void StealFaceMordomo()
    {

        if (nearbyEnemy == null) return;

        Transform inimigoTransform = nearbyEnemy.transform;

        Vector3 direcaoDoInimigo = inimigoTransform.forward;
        Vector3 direcaoParaPlayer = (transform.position - inimigoTransform.position).normalized;

        float angulo = Vector3.Angle(direcaoDoInimigo, direcaoParaPlayer);

        if (angulo < anguloDeVisao)
        {
            // Está na frente → Game Over
            SceneManager.LoadScene(nomeCenaGameOverInterna);
        }

        else
        {
            print("aaaaaaaaaaaaaaa");

            playerFaceUI.sprite = nearbyEnemy.faceTexture; // Atualiza o rosto do player
            //Destroy(nearbyEnemy.gameObject); // Remove o inimigo
            //nearbyEnemy = null;
            //RoubarRostoButton.SetActive(false);
            PlayerFaceManager.Instance.currentFace = "Mordomo";
            Instantiate(birdsPrefab, headPositionMordomo.position, Quaternion.identity, headPositionMordomo);
            Mordomotonto.AtivarTontura();
            MordomoScript.GetComponent<RandomPatrol>().enabled = false;
            PlayerRef.GetComponent<FaceSteal>().enabled = false;
            RoubouFaceMordomo = true;
            dialogoMordomo.GetComponent<DialogoMordomo>().enabled = false;
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            audioSource.PlayOneShot(SomRouboDeFace);
            RoubarRostoButton.SetActive(false);
        }
        if(RoubouFaceMordomo == true)
            PlayerRef.GetComponent<FaceSteal>().enabled = false;
        MordomoScript.GetComponent<RandomPatrol>().enabled = false;

       


    }


    public void StealFaceBaronesa()
    {

        if (nearbyEnemy == null) return;

        Transform inimigoTransform = nearbyEnemy.transform;

        Vector3 direcaoDoInimigo = inimigoTransform.forward;
        Vector3 direcaoParaPlayer = (transform.position - inimigoTransform.position).normalized;

        float angulo = Vector3.Angle(direcaoDoInimigo, direcaoParaPlayer);

        if (angulo < anguloDeVisao)
        {
            // Está na frente → Game Over
            SceneManager.LoadScene(nomeCenaGameOverInterna);
        }

        else
        {
            Destroy(Barreira1);
            Destroy(Barreira2);
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            print("roubou face baronesa");
            Destroy(ConversarButton);
            dialogoBaronesa.PodeRoubarFace = false;
            playerFaceUI.sprite = nearbyEnemy.faceTexture; // Atualiza o rosto do player
            //Destroy(nearbyEnemy.gameObject); // Remove o inimigo
            //nearbyEnemy = null;
            //RoubarRostoButton.SetActive(false);
            PlayerFaceManager.Instance.currentFace = "Baronesa";
            Instantiate(birdsPrefab, headPositionBaronesa.position, Quaternion.identity, headPositionBaronesa);
            Baronesatonta.AtivarTontura();
            BaronesaScript.GetComponent<RandomPatrol>().enabled = false;
            audioSource.PlayOneShot(SomRouboDeFace);
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            Baronesatonta.AtivarTontura();
            RoubarRostoBaronesa.SetActive(false);
            PlayerRef.GetComponent<FaceSteal>().enabled = false;
            
            Baronesa.GetComponent<EnemyAI>().enabled = false;

        }

    }
}
