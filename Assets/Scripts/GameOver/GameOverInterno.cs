using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverInterno : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void JogarNovamente()
    {
        SceneManager.LoadScene("InteriorDaCasa");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
