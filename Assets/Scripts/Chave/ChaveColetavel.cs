using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChaveColetavel : MonoBehaviour
{
    public GameObject botaoColetar; // Botão da UI para coletar a chave
    private bool playerPerto = false;
    public GameObject chaveCanvas;
    private PlayerColisionGeneral playerScript;
    public GameObject player; // Referência ao player
    public AudioSource audioSource;
    public AudioClip SomChaveColetada;
    public bool ColetouChave;
    public GameObject Chave;
    

    private void Start()
    {
        botaoColetar.SetActive(false); // Garante que o botão inicie desativado
        playerScript = player.GetComponent<PlayerColisionGeneral>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            botaoColetar.SetActive(true); // Ativa o botão quando o player se aproxima
            playerPerto = true;
            chaveCanvas.SetActive(true); // ativa o Canvas quando o jogador entra
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            botaoColetar.SetActive(false); // Esconde o botão ao sair da colisão
            playerPerto = false;
            chaveCanvas.SetActive(false); // Desativa o Canvas quando o jogador sai
        }
    }

    public void ColetarChave()
    {
        if (playerPerto)
        {
            if (audioSource != null && Chave != null)
            {
                audioSource.PlayOneShot(SomChaveColetada);
                StartCoroutine(EsperarEFazerDesativacao());

                Debug.Log("Chave coletada!");
                botaoColetar.SetActive(false);
                UIManager.Instance.MostrarMensagem("Você coletou a chave da cozinha");
                chaveCanvas.SetActive(false); // Desativa o Canvas quando o jogador sai
                                              //SceneManager.LoadScene("FimFase1");
                playerScript.TemAChave = true;
                ColetouChave = true;

            }
        }
    }

    private System.Collections.IEnumerator EsperarEFazerDesativacao()
    {
        yield return new WaitForSeconds(audioSource.clip.length); // Espera a duração do som
        Chave.SetActive(false); // Desativa o objeto
    }
}
