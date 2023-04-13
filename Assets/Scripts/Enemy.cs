using UnityEngine;
using System;

[RequireComponent(typeof(PoolObject))]

public class Enemy : MonoBehaviour
{
    private float hp;
    private float fullHp;
    private PoolObject poolObject;
    private EnemyFollow enemyFollow;
    private EnemySize enemySize;
    private EnemyColor enemyColor;

    public static Action onDeath;
    public static Action<Vector3> onDeathPoint;
    public Action<float, float> onHp;

    private void Awake()
    {
        poolObject = GetComponent<PoolObject>();
        enemyFollow = GetComponent<EnemyFollow>();
        enemySize = GetComponent<EnemySize>();
        enemyColor = GetComponent<EnemyColor>();
    }

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
        fullHp = randomData.hp * PlayerPrefs.GetFloat("enemyHpMultiplier");
        hp = fullHp;
        enemyFollow.SetSpeed(randomData.speed);
        enemySize.SetSize(randomData.sizeFrom, randomData.sizeTo);
        enemyColor.SetColor(randomData.rFrom, randomData.gFrom, randomData.bFrom, randomData.rTo, randomData.gTo, randomData.bTo);
        onHp?.Invoke(hp, fullHp);
    }

    public void Death()
    {
        onDeathPoint?.Invoke(transform.position);
        onDeath?.Invoke();
        enemyFollow.enabled = true;
        poolObject.ReturnToPool();
    }
}
