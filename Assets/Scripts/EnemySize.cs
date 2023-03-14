using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySize : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;
    private float max;
    private float min = 0.75f;
    private float current;

    private void OnEnable()
    {
        Enemy.onHp += SizeChanger;
    }

    private void OnDisable()
    {
        Enemy.onHp -= SizeChanger;
    }

    private void SizeChanger(float hp, float fullHp)
    {
        current = max - ((max - min) / fullHp * (fullHp - hp));
        transform.localScale = new Vector3(current, current, current);
    }

    public void SetSize(float from, float to)
    {
        max = Random.Range(from, to);
        transform.localScale = new Vector3(max, max, max);
    }
}
