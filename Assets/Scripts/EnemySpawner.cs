using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemiesList = new List<EnemyData>();
    [SerializeField] private List<Transform> spawnPoint = new List<Transform>();
    [SerializeField] private Transform player;
    [SerializeField] private Transform bonfire;
    [SerializeField] private Transform rageSpawnPoint;
    private bool rage = false;
    private float delay = 4;
    private Pool pool;

    private void Start()
    {
        pool = GetComponent<Pool>();
        SetDelay();
        StartCoroutine(Delay());
    }

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += Rage;
        Enemy.onDeath += SetDelay;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee -= Rage;
        Enemy.onDeath -= SetDelay;
    }

    private EnemyData GetSpawnedEnemy()
    {
        int randomNumber = Random.Range(1, 101);
        List<EnemyData> possibleEnemies = new List<EnemyData>();
        foreach (EnemyData item in enemiesList)
        {
            if (randomNumber <= item.spawnChance)
            {
                possibleEnemies.Add(item);
            }
        }
        EnemyData spawnedEnemy = possibleEnemies[Random.Range(0, possibleEnemies.Count)];
        return spawnedEnemy;
    }

    public void InstantiateEnemy()
    {
        if(rage == false)
        {
            int randomCount = Random.Range(1, 101);

            int randomSpawnPoint = Random.Range(0, spawnPoint.Count);
            EnemyData spawnedEnemy = GetSpawnedEnemy();
            PoolObject enemyGameObject = pool.GetFreeElement(spawnPoint[randomSpawnPoint].position, Quaternion.identity);
            enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);

            if(randomCount <= 75)
            {
                randomSpawnPoint = Random.Range(0, spawnPoint.Count);
                spawnedEnemy = GetSpawnedEnemy();
                enemyGameObject = pool.GetFreeElement(spawnPoint[randomSpawnPoint].position, Quaternion.identity);
                enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);

                if (randomCount <= 50)
                {
                    randomSpawnPoint = Random.Range(0, spawnPoint.Count);
                    spawnedEnemy = GetSpawnedEnemy();
                    enemyGameObject = pool.GetFreeElement(spawnPoint[randomSpawnPoint].position, Quaternion.identity);
                    enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);

                    if (randomCount <= 25)
                    {
                        randomSpawnPoint = Random.Range(0, spawnPoint.Count);
                        spawnedEnemy = GetSpawnedEnemy();
                        enemyGameObject = pool.GetFreeElement(spawnPoint[randomSpawnPoint].position, Quaternion.identity);
                        enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);

                        if (randomCount <= 5)
                        {
                            randomSpawnPoint = Random.Range(0, spawnPoint.Count);
                            spawnedEnemy = GetSpawnedEnemy();
                            enemyGameObject = pool.GetFreeElement(spawnPoint[randomSpawnPoint].position, Quaternion.identity);
                            enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);
                        }
                    }
                }
            }
        }
        if(rage == true)
        {
            if(player.position.x > bonfire.position.x)
            {
                if (player.position.y > bonfire.position.y)
                {
                    rageSpawnPoint.position = new Vector2(player.position.x + 5, player.position.y + 5);
                }
                if (player.position.y < bonfire.position.y)
                {
                    rageSpawnPoint.position = new Vector2(player.position.x + 5, player.position.y - 5);
                }
            }
            if (player.position.x < bonfire.position.x)
            {
                if (player.position.y < bonfire.position.y)
                {
                    rageSpawnPoint.position = new Vector2(player.position.x - 5, player.position.y - 5);
                }
                if (player.position.y > bonfire.position.y)
                {
                    rageSpawnPoint.position = new Vector2(player.position.x - 5, player.position.y + 5);
                }
            }
            EnemyData spawnedEnemy = GetSpawnedEnemy();
            PoolObject enemyGameObject = pool.GetFreeElement(rageSpawnPoint.position, Quaternion.identity);
            enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        InstantiateEnemy();
        StartCoroutine(Delay());
    }

    private void Rage(bool canSee)
    {
        if(canSee == true)
        {
            rage = false;
            SetDelay();
        }
        if(canSee == false)
        {
            rage = true;
            delay = 1f;
        }
    }

    private void SetDelay()
    {
        if(delay >= 1 && !rage)
        {
            delay = 4 - PlayerPrefs.GetFloat("enemySpawnRateMultiplier");
        }
    }
}
