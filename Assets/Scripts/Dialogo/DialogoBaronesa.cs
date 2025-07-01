using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Rendering;
#endif
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DialogoBaronesa : MonoBehaviour
{
    public bool TerminouDialogoBilharvolta;
    public NavMeshAgent agente;
    public Transform PontoPertoBilhar;

    public string[] storyLinesPosBilhar; // Texto alternativo para quando o player NÃO tem a face
    public string[] storyLinesSemFace; // Texto alternativo para quando o player NÃO tem a face
    public string[] dialogoAtual;
    public string[] storyLinesPosPuzzleGeladeira; // Texto alternativo para quando o player NÃO tem a face

    public TextMeshProUGUI storyText; // Referência para o texto

    public string[] storyLines; // Array de textos da história
    public float typingSpeed = 0.05f; // Velocidade de digitação do texto

    public AudioSource audioSource; // Referência para o AudioSource
    public List<AudioClip> typingSounds; // Lista de sons de digitação

    private int currentLine = 0; // Linha atual da história
    private bool isTyping = false; // Controla se o texto está sendo digitado
    public float soundCooldown = 2f; // Intervalo mínimo entre sons
    public float lastSoundTime = 0f; // Tempo do último som tocado
    public GameObject RefInteragirButton;
    public GameObject DialogoBaronesaImage;
    public GameObject DialogoBaronesaText;
    public GameObject DialogoBaronesaButton;
    public GameObject BaronesaScript;
    public GameObject PlayerRef;
    public FaceSteal faceSteal;
    public bool DialogoTerminou = false;
    public BoxCollider boxCollider;
    public bool rouboufacemordomo;
    public bool PodeRoubarFace;
    public bool DialogoComFaceTerminou;
    public GameObject Chave;
    public PuzzleVerificador puzzleVerificador;
    public GameObject PuzzleVerifica;
    public GameManagerSinuca gamemanagerSinuca;
    public GameObject GameManagerSinuca;

    public DialogoBaronesa dialogoBaronesa;

    

    void Start()
    {
        dialogoBaronesa = BaronesaScript.GetComponent<DialogoBaronesa>();
        faceSteal = PlayerRef.GetComponent<FaceSteal>();
        puzzleVerificador = PuzzleVerifica.GetComponent<PuzzleVerificador>();
        rouboufacemordomo = faceSteal.RoubouFaceMordomo;
        gamemanagerSinuca = GameManagerSinuca.GetComponent<GameManagerSinuca>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isTyping && DialogoBaronesaImage.activeInHierarchy) // Clique do mouse
        {
            AdvanceStory();
        }
    }

    void AdvanceStory()
    {
        currentLine++;

        if (currentLine < dialogoAtual.Length)
        {
            StartCoroutine(TypeLine(dialogoAtual[currentLine]));
        }
        else
        {
            EndCutscene();
        }
    }


    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        storyText.text = ""; // Limpa o texto anterior

        foreach (char letter in line.ToCharArray())
        {
            storyText.text += letter;
            // Toca um som aleatório de digitação
            if (typingSounds.Count > 0 && audioSource != null && Time.time - lastSoundTime >= soundCooldown)
            {
                AudioClip randomSound = typingSounds[Random.Range(0, typingSounds.Count)];
                audioSource.PlayOneShot(randomSound);
                lastSoundTime = Time.time;
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndCutscene()
    {
        if (dialogoAtual == storyLinesSemFace)
        {

            DialogoBaronesaImage.SetActive(false);
            DialogoBaronesaText.SetActive(false);
            Debug.Log("Cutscene Finalizada sem face!");
            DialogoTerminou = true;
            PlayerRef.GetComponent<FaceSteal>().enabled = false;
        }
        if (dialogoAtual == storyLines)
        {
            Debug.Log("Cutscene Finalizada com face!");
            // Aqui você pode adicionar o que acontece depois da cutscene (ex.: carregar cena, fechar painel, etc.)
            //SceneManager.LoadScene(2);
            DialogoBaronesaImage.SetActive(false);
            DialogoBaronesaText.SetActive(false);
            DialogoBaronesaButton.SetActive(false);
            //BaronesaScript.GetComponent<RandomPatrol>().enabled = true;
            PlayerRef.GetComponent<FaceSteal>().enabled = false;
            DialogoTerminou = true;
            boxCollider.size = new Vector3(1.28f, 2.02f, 2.91f); // Novo tamanho
            boxCollider.center = new Vector3(0f, 1f, -0.01f); // Novo lugar
            PodeRoubarFace = false;
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            Chave.SetActive(true);
        }
        if (dialogoAtual == storyLinesPosPuzzleGeladeira)
        {
            DialogoBaronesaImage.SetActive(false);
            DialogoBaronesaText.SetActive(false);
            DialogoBaronesaButton.SetActive(false);
            agente.SetDestination(PontoPertoBilhar.position); // Vai para o destino perto do bilhar
            PlayerRef.GetComponent<FaceSteal>().enabled = false;
            PodeRoubarFace = false;
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            gamemanagerSinuca.GetComponent<GameManagerSinuca>().enabled = true;
            TerminouDialogoBilharvolta = true;
        }

        if (dialogoAtual == storyLinesPosBilhar)
        {
            
            DialogoBaronesaImage.SetActive(false);
            DialogoBaronesaText.SetActive(false);
            DialogoBaronesaButton.SetActive(false);
            PlayerRef.GetComponent<FaceSteal>().enabled = true;
            PodeRoubarFace = true;
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            BaronesaScript.GetComponent<RandomPatrol>().enabled = true;
            dialogoBaronesa.GetComponent<DialogoBaronesa>().enabled = false;
            Destroy(RefInteragirButton);
        }

     

    }
    public void StartDialogoBaronesa()
    {
        if (faceSteal.RoubouFaceMordomo == true)
        {
            DialogoBaronesaImage.SetActive(true);
            DialogoBaronesaText.SetActive(true);
            RefInteragirButton.SetActive(false);
            dialogoAtual = storyLines;
        }
        else
        {
            DialogoBaronesaImage.SetActive(true);
            DialogoBaronesaText.SetActive(true);
            RefInteragirButton.SetActive(false);
            dialogoAtual = storyLinesSemFace;
        }

        if (puzzleVerificador.PuzzleGeladeiraTerminou == true)
        {
            DialogoBaronesaImage.SetActive(true);
            DialogoBaronesaText.SetActive(true);
            RefInteragirButton.SetActive(false);
            dialogoAtual = storyLinesPosPuzzleGeladeira;
        }
        if (gamemanagerSinuca.TerminouBilhar == true)
        {
            DialogoBaronesaImage.SetActive(true);
            DialogoBaronesaText.SetActive(true);
            RefInteragirButton.SetActive(false);
            dialogoAtual = storyLinesPosBilhar;
        }
        currentLine = 0;

        if (dialogoAtual.Length > 0)
            StartCoroutine(TypeLine(dialogoAtual[0]));
    }
}




