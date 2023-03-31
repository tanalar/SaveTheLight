using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]

public class MinigunBullet : MonoBehaviour
{
    private float damage;
    private PoolObject poolObject;

    private void Start()
    {
        poolObject = GetComponent<PoolObject>();
    }

    private void OnEnable()
    {
        SetValues();
        StartCoroutine("Destroy");
    }
    private void OnDisable()
    {
        StopCoroutine("Destroy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
            //Destroy(gameObject);
            poolObject.ReturnToPool();
        }
        if (collision.gameObject.tag == "Wall")
        {
            //Destroy(gameObject);
            poolObject.ReturnToPool();
        }
    }

    private void SetValues()
    {
        damage = PlayerPrefs.GetFloat("minigunDamage");
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        poolObject.ReturnToPool();
    }
}
