// EventBus.cs
using System;
using System.Collections.Generic;

public class EventBus
{
    private static EventBus _instance;
    public static EventBus Instance => _instance ??= new EventBus();

    private readonly Dictionary<Type, List<Delegate>> _subscriptions = new();

    private EventBus() { } // приватный конструктор

    public void Subscribe<T>(Action<T> handler) where T : IEvent
    {
        Type eventType = typeof(T);
        if (!_subscriptions.ContainsKey(eventType))
            _subscriptions[eventType] = new List<Delegate>();

        _subscriptions[eventType].Add(handler);
    }

    public void Unsubscribe<T>(Action<T> handler) where T : IEvent
    {
        Type eventType = typeof(T);
        if (_subscriptions.TryGetValue(eventType, out var handlers))
        {
            handlers.Remove(handler);
            if (handlers.Count == 0)
                _subscriptions.Remove(eventType);
        }
    }

    public void Publish<T>(T eventData) where T : IEvent
    {
        Type eventType = typeof(T);
        if (_subscriptions.TryGetValue(eventType, out var handlers))
        {
            // Копируем список для безопасного вызова (защита от изменений в процессе)
            var handlersCopy = new List<Delegate>(handlers);
            foreach (var handler in handlersCopy)
            {
                (handler as Action<T>)?.Invoke(eventData);
            }
        }
    }
}