using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<EnemyData> enemiesList = new List<EnemyData>();
    [SerializeField] private List<Transform> spawnPoint = new List<Transform>();
    [SerializeField] private Transform player;
    [SerializeField] private Transform bonfire;
    [SerializeField] private Transform rageSpawnPoint;
    private bool rage = false;
    private float delay = 4;

    private void Start()
    {
        SetDelay();
        StartCoroutine(Delay());
    }

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += RageOff;
        Bonfire.onPlayerCanNotSee += RageOn;
        Values.onSetValues += SetDelay;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee -= RageOff;
        Bonfire.onPlayerCanNotSee -= RageOn;
        Values.onSetValues -= SetDelay;
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
            int randomNumber = Random.Range(0, spawnPoint.Count);
            EnemyData spawnedEnemy = GetSpawnedEnemy();
            GameObject enemyGameObject = Instantiate(enemyPrefab, spawnPoint[randomNumber].position, Quaternion.identity);
            enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);
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
            GameObject enemyGameObject = Instantiate(enemyPrefab, rageSpawnPoint.position, Quaternion.identity);
            enemyGameObject.GetComponent<Enemy>().SetValues(spawnedEnemy);
        }
    }

    private IEnumerator Delay()
    {
        InstantiateEnemy();
        yield return new WaitForSeconds(delay);
        StartCoroutine(Delay());
    }

    private void RageOn()
    {
        rage = true;
        delay = 1.5f;
    }

    private void RageOff()
    {
        rage = false;
        SetDelay();
    }

    private void SetDelay()
    {
        if(delay > 1)
        {
            delay = 4 - PlayerPrefs.GetFloat("enemySpawnRateMultiplier");
        }
    }
}
