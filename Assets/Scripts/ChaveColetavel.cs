using UnityEngine;
using UnityEngine.UI;

public class ChaveColetavel : MonoBehaviour
{
    public GameObject botaoColetar; // Botão da UI para coletar a chave
    private bool playerPerto = false;
    public GameObject chaveCanvas;

    private void Start()
    {
        botaoColetar.SetActive(false); // Garante que o botão inicie desativado
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
            Debug.Log("Chave coletada!");
            botaoColetar.SetActive(false);
            gameObject.SetActive(false); // Esconde a chave do cenário
            UIManager.Instance.MostrarMensagem("Você coletou a chave!");
            chaveCanvas.SetActive(false); // Desativa o Canvas quando o jogador sai
        }
    }
}
