using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraDetector : MonoBehaviour
{
    public string tagDoPlayer = "Player";
    public Light spotLight;
    public string cenaGameOver = "GameOver"; // Altere conforme o nome da sua cena

    private bool detectado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!detectado && other.CompareTag("Player"))
        {
            detectado = true;
            Debug.Log("Jogador detectado!");
            spotLight.color = Color.red;
            Invoke("CarregarGameOver", 1.5f); // Pequeno atraso
        }
    }

    void CarregarGameOver()
    {
        SceneManager.LoadScene(cenaGameOver);
    }
}
