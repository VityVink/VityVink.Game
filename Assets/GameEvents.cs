using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    public static class EventBus
    {
        
    }
    public struct EntityKilledEvent
    {
        public string EntityID;
        public string EntityName;
        public string EntityType;
        public Vector3 EntityPosition;

        public EntityKilledEvent(string id, string name, string type, Vector3 position)
        {
            EntityID = id; 
            EntityName = name; 
            EntityType = type;
            EntityPosition = position;
        }
    }
    public struct ItemCollectedEvent
    {
        public string ItemID; 
        public string ItemName;
        public string ItemType;
        public int ItemCount;
        public Vector3 ItemPosition;

        public ItemCollectedEvent(string id, string name, string type, int count, Vector3 position)
        {
            ItemID = id;
            ItemName = name;
            ItemType = type;
            ItemCount = count;
            ItemPosition = position;
        }
    }
    public struct NPCTalkedEvent
    {
        public string NPCID;
        public string NPCName;
        public string NPCType;
        public string NPCDialogueNode;
        public Vector3 NPCPosition;

        public NPCTalkedEvent(string id, string name,string type, string dialogueNode, Vector3 position)
        {
            NPCID = id;
            NPCName = name;
            NPCType = type;
            NPCDialogueNode = dialogueNode;
            NPCPosition = position;
        }
    }
    public struct PlayerReachedPointEvent
    {
        public string PointID;
        public string PointName;
        public string PointType;
        public Vector3 PointPosition;

        public PlayerReachedPointEvent(string id, string name, string type, Vector3 position)
        {
            PointID = id;
            PointName = name;
            PointType = type;
            PointPosition = position;
        }
    }
    public struct QuestCompletedEvent
    {
        public string QuestID;
        public string QuestName;

        public QuestCompletedEvent(string id, string name)
        {
            QuestID = id;
            QuestName = name;
        }
    }
}
