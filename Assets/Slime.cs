using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private string enemyName = "Slime";
    public QuestManager questManager = FindObjectOfType<QuestManager>();

    private void Die()
    {
        questManager.OnEnemyKilled(enemyName);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            Die();
    }
}
