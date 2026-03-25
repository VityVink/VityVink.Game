using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    public static class EventBus
    {
        private static Dictionary<Type, Delegate> _subscribers = new Dictionary<Type, Delegate>();

        public static void Subscribe<T>(Action<T> handler) where T : struct
        {
            Type eventType = typeof(T);

            if (_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = Delegate.Combine(_subscribers[eventType], handler);
            }
            else
            {
                _subscribers[eventType] = handler;
            }
        }
        public static void Unsubscribe<T>(Action<T> handler) where T : struct
        {
            Type eventType = typeof(T);

            if (_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = Delegate.Remove(_subscribers[eventType], handler);
                if (_subscribers[eventType] == null)
                {
                    _subscribers.Remove(eventType);
                }
            }
        }
        public static void Publish<T>(T eventData) where T : struct
        {
            Type eventType = typeof(T);

            if (_subscribers.ContainsKey(eventType))
            {
                var handler = _subscribers[eventType] as Action<T>;
                handler?.Invoke(eventData);
            }
        }
        public static void Clear()
        {
            _subscribers.Clear();
        }
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
