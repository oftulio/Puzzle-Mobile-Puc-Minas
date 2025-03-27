using UnityEngine;
using System.Collections.Generic;

public class PianoManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] pianoSounds; // Sons das teclas
    private List<string> playerInput = new List<string>();
    private string[] correctSequence = { "Do1", "Mi", "Re", "Sol", "Si", "La", "Mi", "Fa" };

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
                Debug.Log("Puzzle Resolvido!");
                PuzzleCompleted();
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