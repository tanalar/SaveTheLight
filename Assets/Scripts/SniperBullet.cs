using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]

public class SniperBullet : MonoBehaviour
{
    private float damage;
    private float knockback;
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
        if (collision.gameObject.TryGetComponent<Knockback>(out Knockback knockbackComponent))
        {
            knockbackComponent.PlayFeedback(gameObject, knockback);
        }
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
        damage = PlayerPrefs.GetFloat("sniperDamage");
        knockback = PlayerPrefs.GetFloat("sniperKnockback");
        fireForce = 30 + (PlayerPrefs.GetFloat("sniperFireRate") * 65);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        poolObject.ReturnToPool();
    }
}
