using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class LootBag : MonoBehaviour
{
    [SerializeField] private List<LootData> lootList = new List<LootData>();
    private Pool pool;

    private void Start()
    {
        pool = GetComponent<Pool>();
    }

    private void OnEnable()
    {
        Enemy.onDeathPoint += InstantiateLoot;
    }
    private void OnDisable()
    {
        Enemy.onDeathPoint -= InstantiateLoot;
    }

    private LootData GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<LootData> possibleItems = new List<LootData>();
        foreach (LootData item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        LootData droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
        return droppedItem;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        LootData droppedItem = GetDroppedItem();
        PoolObject lootGameObject = pool.GetFreeElement(spawnPosition, Quaternion.identity);
        lootGameObject.GetComponent<Energy>().SetValues(droppedItem);
    }
}
