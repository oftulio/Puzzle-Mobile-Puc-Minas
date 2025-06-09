using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorInteractionMobile : MonoBehaviour
{
    public string requiredFace = "Jardineiro";
    public string nextSceneName = "InteriorDaCasa";

    public GameObject messageUI; // Mensagem de "N�o possui a face necess�ria"
    public GameObject interactionButton; // Refer�ncia ao bot�o de intera��o (UI)

    private bool playerNearby = false;
    public GameObject TransitionPainel;

    void Start()
    {
        if (interactionButton != null)
        {
            interactionButton.SetActive(false);
            // Adiciona listener no bot�o
            interactionButton.GetComponent<Button>().onClick.AddListener(OnInteract);
        }
    }

     public void OnInteract()
    {
        if (!playerNearby) return;

        if (PlayerFaceManager.Instance.currentFace == requiredFace)
        {
            SceneManager.LoadScene(nextSceneName);
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            Object.FindAnyObjectByType<SceneTransition>().TransitionToScene("nextSceneName");
            TransitionPainel.SetActive(false);

        }
        else
        {
            if (messageUI != null)
            {
                messageUI.SetActive(true);
                Invoke("HideMessage", 2f);
            }
        }
    }

    void HideMessage()
    {
        if (messageUI != null)
        {
            messageUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (interactionButton != null)
            {
                interactionButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (interactionButton != null)
            {
                interactionButton.SetActive(false);
            }
        }
    }
}
