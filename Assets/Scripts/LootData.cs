using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Loot", order = 2)]
public class LootData : ScriptableObject
{
    public float rFrom;
    public float gFrom;
    public float bFrom;
    public float rTo;
    public float gTo;
    public float bTo;
    public int dropChance;
    public float scaleMultiplier;
    public int value;
}
