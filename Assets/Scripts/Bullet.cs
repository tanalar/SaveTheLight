using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]

public class Bullet : MonoBehaviour
{
    private float fireForce;
    private float damage;
    [SerializeField]private float knockback;
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
            //Destroy(gameObject);
            poolObject.ReturnToPool();
        }
        if(collision.gameObject.tag == "Wall")
        {
            //Destroy(gameObject);
            poolObject.ReturnToPool();
        }
    }

    private void SetValues()
    {
        damage = PlayerPrefs.GetFloat("playerDamage");
        knockback = PlayerPrefs.GetFloat("playerKnockback");
        fireForce = 12.5f + (PlayerPrefs.GetFloat("playerFireRate") * 40);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        poolObject.ReturnToPool();
    }
}
