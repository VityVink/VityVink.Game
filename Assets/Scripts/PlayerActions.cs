using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // ќбъ€вление событи€: делегат System.Action (нет возвращаемого значени€)
    // ≈диное событие, где отслеживаетс€ кол-во
    public event System.Action<string, int> OnAction; 

    // —обытие дл€ однократных триггеров
    public event System.Action<string> OnTrigger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnAction?.Invoke("Gold", 10);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnTrigger?.Invoke("Blacksmith");
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnAction?.Invoke("Goblin", 1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnTrigger?.Invoke("Tavern");
        }

    }
}
