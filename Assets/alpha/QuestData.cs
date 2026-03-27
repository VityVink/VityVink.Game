using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Simple Quest")]
public class QuestData : ScriptableObject
{
    public string iD;
    public string title;
    public string description;
    public List<TaskData> tasks;
}
