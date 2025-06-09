using UnityEngine;
using UnityEngine.UI;

public class PianoPuzzle : MonoBehaviour
{
    public GameObject pianoCanvas;
    public GameObject puzzlePanel; // Painel do puzzle
    public GameObject player; // Referência ao player
    private MobileLook playerScript; // Acessa o Script do player, cuidado caso as funcoes estiverem privadas nao podem ser modificadas, coloquei bools no MobileLook para desativar e ativar os touche de movimento e girar
    public GameObject joyStick; // Joystick mobile
    public Camera mainCamera; // Câmera principal
    private bool playerNearby = false;
    private void Start()
    {
        playerScript = player.GetComponent<MobileLook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // playerNearby = true;
           GameManager.ins.ButtonPiano.SetActive(true);
            pianoCanvas.SetActive(true); // ativa o Canvas quando o jogador entra
        }
 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             //playerNearby = false;
            GameManager.ins.ButtonPiano.SetActive(false); // acessa o scrit do GameManager e suas dependencias
            pianoCanvas.SetActive(false); // desativa o Canvas quando o jogador sai
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
        //player.GetComponent<VirtualJoystick>().enabled = false; // Desativa movimentação
        joyStick.SetActive(false); // Oculta joystick
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
        //mainCamera.GetComponent<MobileLook>().enabled = false; // Desativa rotação da câmera
        Debug.Log(joyStick);
    }

    public void ClosePuzzle()
    {
        puzzlePanel.SetActive(false);
        //player.GetComponent<VirtualJoystick>().enabled = true;
        joyStick.SetActive(true);
        playerScript.moveCamera = true;
        playerScript.movePlayer = true;
        //mainCamera.GetComponent<MobileLook>().enabled = true;
        Object.FindAnyObjectByType<PianoManager>().ResetPuzzle(); // Reseta contagem de teclas
        Debug.Log(joyStick);
        print("Alouuuuuu");
    }
}