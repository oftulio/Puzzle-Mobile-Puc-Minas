using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DialogoMordomo : MonoBehaviour
{
    
    public TextMeshProUGUI storyText; // Referência para o texto
    
    public string[] storyLines; // Array de textos da história
    public float typingSpeed = 0.05f; // Velocidade de digitação do texto

    public AudioSource audioSource; // Referência para o AudioSource
    public List<AudioClip> typingSounds; // Lista de sons de digitação

    private int currentLine = 0; // Linha atual da história
    private bool isTyping = false; // Controla se o texto está sendo digitado
    public float soundCooldown = 2f; // Intervalo mínimo entre sons
    public float lastSoundTime = 0f; // Tempo do último som tocado
    public GameObject DialogoMordmoImage;
    public GameObject DialogoMordmoText;    
    public GameObject DialogoMordmoButton;
    public GameObject MordomoScript;
    public GameObject PlayerRef;
    public bool DialogoTerminou = false;
    public BoxCollider boxCollider;
    public GameObject Baronesa;
    public GameObject RefInteragirButton;

    void Start()
    {
        

    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isTyping && DialogoMordmoImage.activeInHierarchy) // Clique do mouse
        {
            AdvanceStory();
        }
    }

    void AdvanceStory()
    {
        currentLine++;

        if (currentLine < storyLines.Length) // Se ainda há mais texto
        {

            StartCoroutine(TypeLine(storyLines[currentLine]));
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
        Debug.Log("Cutscene Finalizada pixxxxxx!");
        // Aqui você pode adicionar o que acontece depois da cutscene (ex.: carregar cena, fechar painel, etc.)
        //SceneManager.LoadScene(2);
        Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
        DialogoMordmoImage.SetActive(false);
        DialogoMordmoText.SetActive(false);
        Destroy(DialogoMordmoButton);
        MordomoScript.GetComponent<RandomPatrol>().enabled = true;
        PlayerRef.GetComponent<FaceSteal>().enabled = true;
        DialogoTerminou = true;
        boxCollider.size = new Vector3(1.28f, 2.02f, 2.91f); // Novo tamanho
        boxCollider.center = new Vector3(0f, 1f, -0.01f); // Novo lugar
        Baronesa.SetActive(true);
    }
    public void StartDialogoMordomo()
    {
        DialogoMordmoImage.SetActive(true);
        RefInteragirButton.SetActive(false);
        if (storyLines.Length > 0)
            StartCoroutine(TypeLine(storyLines[0]));

       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            DialogoTerminou = true;
            isTyping = false;
            storyText.text = ""; // Limpa o texto anterior
            currentLine = 0;
            audioSource.Stop();
        }

    }

}