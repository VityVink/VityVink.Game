using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestInstance> CurrentQuests = new List<QuestInstance>();
    
    public void TakeQuest(QuestData questData)
    {
        QuestInstance newQuest = new QuestInstance(questData);
        CurrentQuests.Add(newQuest);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
