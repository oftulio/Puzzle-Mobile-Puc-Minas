using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Text questUIText;               // Refer�ncia ao Text da UI que exibe a miss�o
    public Quest[] quests;                 // Lista de todas as miss�es
    private int currentQuestIndex = 0;     // �ndice da miss�o atual

    void Start()
    {
        UpdateQuestUI();                   // Mostra a primeira miss�o ao iniciar
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
                questUIText.text = "Todas as miss�es conclu�das!";
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
