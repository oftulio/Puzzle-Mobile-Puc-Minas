using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text mensagemTexto;
    public GameObject painelMensagem;
    public GameObject painelPause;
    [SerializeField]private GameObject gameOverUI;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        painelPause.SetActive(false);
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
    public void ButtonPlayAgain()
    {
        SceneManager.LoadScene("scene1");
    }
    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);

    }
    public void PauseGame()
    {
        painelPause.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        painelPause.SetActive(false);
        Time.timeScale = 1f;
    }
}
