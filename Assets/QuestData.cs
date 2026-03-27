using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "VityVink.System/New Quest")]
public class QuestData : ScriptableObject
{
    public string Title;
    public string Description;
    public List<TaskData> tasks;
    //public List<Reward> rewards;
}
