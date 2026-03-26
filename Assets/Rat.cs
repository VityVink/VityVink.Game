using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public string RatID = "Rat";

    public void Die()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        questManager.OnEnemyKilled(RatID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die();

        }
    }
}
