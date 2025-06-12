using UnityEngine;
using System.Collections.Generic;
using UnityEngine.ProBuilder.Shapes;
using System.Collections;
using Unity.Cinemachine;
using UnityEditor.Rendering;
using UnityEngine.UI;

public class PianoManager : MonoBehaviour
{
    
    public GameObject PianoButton;
    public GameObject PianoPainel;
    public AudioSource audioSource;
    public AudioClip[] pianoSounds; // Sons das teclas
    private List<string> playerInput = new List<string>();
    private string[] correctSequence = { "Do1", "Mi", "Re", "Sol", "Si", "La", "Mi", "Fa" };
    public AudioClip SomQuadroCaindo;
    [SerializeField] private PortaInternaSalao door1;
    [SerializeField] private PortaInternaSalao door2;
    public PianoPuzzle pianoPuzzle;
    public GameObject Mordomo;

   

    public void PlayKey(string keyName)
    {
        int index = GetKeyIndex(keyName);
        if (index != -1)
        {
            audioSource.PlayOneShot(pianoSounds[index]); // Toca som
        }

        playerInput.Add(keyName);

        if (playerInput.Count == correctSequence.Length)
        {
            if (IsSequenceCorrect())
            {
                Destroy(PianoButton); 
                Debug.Log("Puzzle Resolvido!");
                PuzzleCompleted();
                Object.FindAnyObjectByType<QuestManager>().CompletePuzzle();
                

            }
            else
            {
                Debug.Log("Sequência errada, resetando...");
                ResetPuzzle();
            }
        }
    }

    private bool IsSequenceCorrect()
    {
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    public void ResetPuzzle()
    {
        playerInput.Clear();
    }

    private void PuzzleCompleted()
    {
        Debug.Log("Parabéns! O puzzle foi resolvido.");
        
            audioSource.PlayOneShot(SomQuadroCaindo);
            door1.IsOpen();
            door2.IsOpen();
            pianoPuzzle.ClosePuzzle();
            Object.FindAnyObjectByType<QuestManager>().CompleteCurrentQuest();
            Mordomo.SetActive(true);
        
        
    }

    private int GetKeyIndex(string keyName)
    {
        switch (keyName)
        {
            case "Do1": return 0;
            case "Re": return 1;
            case "Mi": return 2;
            case "Fa": return 3;
            case "Sol": return 4;
            case "La": return 5;
            case "Si": return 6;
            case "Do2": return 7;
            default: return -1;
        }
    }

    
   
}