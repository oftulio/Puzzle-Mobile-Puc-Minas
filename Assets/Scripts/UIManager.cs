using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text mensagemTexto;
    public GameObject painelMensagem;

    private void Awake()
    {
        Instance = this;
        painelMensagem.SetActive(false);
    }

    public void MostrarMensagem(string mensagem)
    {
        painelMensagem.SetActive(true);
        mensagemTexto.text = mensagem;
        StartCoroutine(EsconderMensagem());
    }

    private IEnumerator EsconderMensagem()
    {
        yield return new WaitForSeconds(2f);
        painelMensagem.SetActive(false);
    }
}
