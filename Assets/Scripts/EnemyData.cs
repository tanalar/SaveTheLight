using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public float spawnChance;
    public float hp;
    public float speed;
    public float sizeFrom;
    public float sizeTo;
    public float rFrom;
    public float gFrom;
    public float bFrom;
    public float rTo;
    public float gTo;
    public float bTo;
}
