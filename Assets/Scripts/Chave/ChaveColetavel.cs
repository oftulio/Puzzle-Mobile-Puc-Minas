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
            audioSource.PlayOneShot(SomChaveColetada);
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
