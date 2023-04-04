using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Loot", order = 2)]
public class LootData : ScriptableObject
{
    public Color color;
    public int dropChance;
    public float scaleMultiplier;
    public int value;
}
