using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public Text questUIText;              // Texto da miss�o
    public Text puzzleUIText;             // Texto do contador de puzzles
    public Quest[] quests;
    private int currentQuestIndex = 0;

    private int completedPuzzles = 0;     // Puzzles resolvidos na miss�o atual
    

    void Start()
    {
        UpdateQuestUI();
        UpdatePuzzleUI();
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuestIndex < quests.Length)
        {
            quests[currentQuestIndex].isCompleted = true;
            currentQuestIndex++;

            completedPuzzles = 0; // Resetar para a nova miss�o

            if (currentQuestIndex < quests.Length)
            {
                UpdateQuestUI();
                UpdatePuzzleUI();
            }
            else
            {
                questUIText.text = "";
                puzzleUIText.text = "";
            }
        }
    }

    private void UpdateQuestUI()
    {
        if (currentQuestIndex < quests.Length)
        {
            questUIText.text = quests[currentQuestIndex].questTitle;
        }
    }

    private void UpdatePuzzleUI()
    {
        if (currentQuestIndex < quests.Length)
        {
            int total = quests[currentQuestIndex].totalPuzzles;
            puzzleUIText.text = "Puzzles conclu�dos: " + completedPuzzles + " / " + total;
        }
    }

    public void CompletePuzzle()
    {
        if (currentQuestIndex < quests.Length)
        {
            completedPuzzles++;
            UpdatePuzzleUI();

            if (completedPuzzles >= quests[currentQuestIndex].totalPuzzles)
            {
                // Pode colocar algo aqui, como desbloquear algo ou completar a miss�o automaticamente
                Debug.Log("Todos os puzzles da miss�o conclu�dos!");
            }
        }
    }
}
