using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Text questUIText;               // Referência ao Text da UI que exibe a missão
    public Quest[] quests;                 // Lista de todas as missões
    private int currentQuestIndex = 0;     // Índice da missão atual

    void Start()
    {
        UpdateQuestUI();                   // Mostra a primeira missão ao iniciar
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuestIndex < quests.Length)
        {
            quests[currentQuestIndex].isCompleted = true;
            currentQuestIndex++;

            if (currentQuestIndex < quests.Length)
            {
                UpdateQuestUI();
            }
            else
            {
                questUIText.text = "Todas as missões concluídas!";
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
}
