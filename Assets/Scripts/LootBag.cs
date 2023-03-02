using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<LootData> lootList = new List<LootData>();

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
        GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
        lootGameObject.GetComponent<Energy>().SetValues(droppedItem);
    }
}
