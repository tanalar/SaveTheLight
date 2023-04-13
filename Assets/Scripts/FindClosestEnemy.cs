using System;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemy : MonoBehaviour
{
    public List<Enemy> enemies;
    public Enemy closestEnemy;

    private void OnEnable()
    {
        Enemy.onDeath += Find;
        Bonfire.onEnemyVisible += Find;
    }
    private void OnDisable()
    {
        Enemy.onDeath -= Find;
        Bonfire.onEnemyVisible -= Find;
    }

    private void Find()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject.tag == "Visible" && enemies[i] != null)
            {
                Vector3 diff = enemies[i].transform.position - position;
                float currentDistance = diff.sqrMagnitude;
                if (distance > currentDistance)
                {
                    closestEnemy = enemies[i];
                    distance = currentDistance;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemies.Add(enemyComponent);
            Find();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemies.Remove(enemyComponent);
            Find();
        }
    }
}
