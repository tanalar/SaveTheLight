using UnityEngine;
using System;

[RequireComponent(typeof(PoolObject))]

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyFollow enemyFollow;
    [SerializeField] CircleCollider2D circleCollider;
    private float hp;
    private float fullHp;
    private EnemyData data;
    private PoolObject poolObject;

    public static Action onDeath;
    public Action<float, float> onHp;

    private void Start()
    {
        poolObject = GetComponent<PoolObject>();
        enemyFollow = GetComponent<EnemyFollow>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void TakeDamage(float damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            onHp?.Invoke(hp, fullHp);
        }
        //if (hp <= 0)
        //{
        //    circleCollider.enabled= false;
        //}
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
        //Destroy(gameObject);
        enemyFollow.enabled = true;
        //circleCollider.enabled = true;
        poolObject.ReturnToPool();
    }
}
