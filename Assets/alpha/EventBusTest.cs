using GameEvents;
using UnityEngine;

public class EventBusTest : MonoBehaviour
{
    void Start()
    {
        EventBus.Subscribe<EntityKilledEvent>(OnEntityKilled);
        EventBus.Subscribe<EntityKilledEvent>(OnEntityKilled);

        Invoke(nameof(SimulateEntityKill), 5f);
    }
    private void SimulateEntityKill()
    {
        Debug.Log($"[Test] Simulated entity kill...");
        var killEvent = new EntityKilledEvent(
            "Goblin_1",
            "Stone Goblin",
            "Goblin",
            new Vector3(19,12,1)
            );
        EventBus.Publish(killEvent);
    }
    private void OnEntityKilled(EntityKilledEvent e)
    {
        Debug.Log($"[Test] Entity Killed! Type: {e.EntityType}, ID: {e.EntityID}, name: {e.EntityName}, position: {e.EntityPosition}");
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<EntityKilledEvent>(OnEntityKilled);
    }
}
