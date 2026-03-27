using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private string enemyName = "Slime";

    private void Die()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        questManager.OnEnemyKilled(enemyName);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Die();
    }
}
