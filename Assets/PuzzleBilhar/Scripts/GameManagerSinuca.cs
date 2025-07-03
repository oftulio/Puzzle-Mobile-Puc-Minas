using UnityEngine;

public class GameManagerSinuca : MonoBehaviour
{
    
    public GameObject painelBilhar, painelDerrota, InteragirBilharButton;
    

    public GameObject player; // Referência ao player
    public MobileLook playerScript; // Acessa o Script do player, cuidado caso as funcoes estiverem privadas nao podem ser modificadas, coloquei bools no MobileLook para desativar e ativar os touche de movimento e girar
    public GameObject joyStick; // Joystick mobile
    public AudioClip SomPuzzleConcluido;
    public AudioSource audioSource;
    public bool TerminouBilhar = false;
    

    public void Start()
    {
        
        playerScript = player.GetComponent<MobileLook>();
        
    }
    private void Awake()
    {
    
    }

    public void AbrirPuzzle()
    {
        painelBilhar.SetActive(true);
        playerScript.moveCamera = false;
        playerScript.movePlayer = false;
        joyStick.SetActive(false); // Oculta joystic
    }
    public void FecharPuzzle()
    {
        painelBilhar.SetActive(false);
        playerScript.moveCamera = true;
        playerScript.movePlayer = true;
        joyStick.SetActive(true); // Oculta joystic
    }

    public void Vitoria()
    {
        playerScript.moveCamera = true;
        playerScript.movePlayer = true;
        painelBilhar.SetActive(false); 
        joyStick.SetActive(true); // mostra joystic
        Destroy(InteragirBilharButton);
        UIManager.Instance.MostrarMensagem("Puzzle Concluído");
        audioSource.PlayOneShot(SomPuzzleConcluido);
        TerminouBilhar = true;
        Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
    }

    public void ResetarBilhar()
    {
        painelDerrota.SetActive(true);
        // Aqui você pode recarregar a cena ou resetar as bolas
    }
}

