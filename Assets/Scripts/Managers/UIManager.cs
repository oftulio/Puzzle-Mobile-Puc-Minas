using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text mensagemTexto;
    public GameObject painelMensagem;
    public GameObject painelPause;
    public GameObject painelMenuSensi;
    public GameObject painelMenuSound;
    public GameObject painelMenuPause;

    public float SpeedSkyBox;

    public GameObject painelPauseGame;
    [SerializeField]private GameObject gameOverUI;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        painelPauseGame.SetActive(true);
        painelPause.SetActive(false);
        painelMensagem.SetActive(false);
        

    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * SpeedSkyBox);
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
    public void PainelPause()
    {
        painelPauseGame.SetActive(true);
        
    }
}
