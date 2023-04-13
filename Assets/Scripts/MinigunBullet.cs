using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]

public class MinigunBullet : MonoBehaviour
{
    private float damage;
    private float fireForce;
    private PoolObject poolObject;

    private void Start()
    {
        poolObject = GetComponent<PoolObject>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * fireForce * Time.deltaTime);
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
            poolObject.ReturnToPool();
        }
        if (collision.gameObject.tag == "Wall")
        {
            poolObject.ReturnToPool();
        }
    }

    private void SetValues()
    {
        damage = PlayerPrefs.GetFloat("minigunDamage");
        fireForce = 7 + (PlayerPrefs.GetFloat("minigunFireRate") * 65);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        poolObject.ReturnToPool();
    }
}
