using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questTitle;       // T�tulo da miss�o
    public string questDescription; // Descri��o completa (opcional)
    public bool isCompleted;        // Status da miss�o
    public int totalPuzzles;  // Total de puzzles desta miss�o
}