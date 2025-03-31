using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void JogarNovamente()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
