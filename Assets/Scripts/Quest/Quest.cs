using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questTitle;       // Título da missão
    public string questDescription; // Descrição completa (opcional)
    public bool isCompleted;        // Status da missão
    public int totalPuzzles;  // Total de puzzles desta missão
}