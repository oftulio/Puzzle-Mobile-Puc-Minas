using UnityEngine;
using UnityEngine.UI;

public class PianoPuzzle : MonoBehaviour
{
    public GameObject puzzlePanel; // Painel do puzzle
    public GameObject player; // Referência ao player
    public GameObject joystick; // Joystick mobile
    public Camera mainCamera; // Câmera principal
    private bool playerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void Update()
    {
        if (playerNearby && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OpenPuzzle();
        }
    }

    public void OpenPuzzle()
    {
        puzzlePanel.SetActive(true);
        player.GetComponent<VirtualJoystick>().enabled = false; // Desativa movimentação
        joystick.SetActive(false); // Oculta joystick
        mainCamera.GetComponent<MobileLook>().enabled = false; // Desativa rotação da câmera
    }

    public void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        player.GetComponent<VirtualJoystick>().enabled = true;
        joystick.SetActive(true);
        mainCamera.GetComponent<MobileLook>().enabled = true;
        Object.FindAnyObjectByType<PianoManager>().ResetPuzzle(); // Reseta contagem de teclas
    }
}