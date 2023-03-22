using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEditor.Rendering;

public class Enemy : MonoBehaviour
{
    private float hp;
    private float fullHp;
    private EnemyData data;
    
    public static Action onDeath;
    public Action<float, float> onHp;

    public void TakeDamage(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            onHp?.Invoke(hp, fullHp);
        }
    }

    public void SetValues(EnemyData randomData)
    {
        data = randomData;
        fullHp = data.hp * PlayerPrefs.GetFloat("enemyHpMultiplier");
        hp = fullHp;
        GetComponent<EnemyFollow>().SetSpeed(data.speed);
        GetComponent<EnemySize>().SetSize(data.sizeFrom, data.sizeTo);
        GetComponent<EnemyColor>().SetColor(data.rFrom, data.gFrom, data.bFrom, data.rTo, data.gTo, data.bTo);
        onHp?.Invoke(hp, fullHp);
    }

    public void Death()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
