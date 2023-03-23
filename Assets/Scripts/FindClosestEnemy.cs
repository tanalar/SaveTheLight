using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FindClosestEnemy : MonoBehaviour
{
    public List<Enemy> enemies;
    public Enemy closestEnemy;
    public static Action onEmpty;
    public static Action onNotEmpty;

    private void OnEnable()
    {
        Enemy.onDeath += Find;
        Bonfire.onEnemyVisible += Find;
        Bonfire.onEnemyInvisible += Find;
    }
    private void OnDisable()
    {
        Enemy.onDeath -= Find;
        Bonfire.onEnemyVisible -= Find;
        Bonfire.onEnemyInvisible -= Find;
    }

    private void Find()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        for (int i = 0; i < enemies.Count; i++)
        {
            //if (enemies[i] == null)
            //{
            //    enemies.RemoveAt(i);
            //    i = 0;
            //}
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
        EnemiesCount();
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

    private void EnemiesCount()
    {
        if(enemies.Count < 1)
        {
            onEmpty?.Invoke();
        }
        if(enemies.Count > 0 && closestEnemy != null)
        {
            onNotEmpty?.Invoke();
        }
    }
}
