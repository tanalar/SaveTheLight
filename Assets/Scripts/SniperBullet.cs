using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    private float damage = 3;
    private float knockback = 10000;

    private void Start()
    {
        //damage = PlayerPrefs.GetFloat("playerDamage");
        //knockback = 100 * PlayerPrefs.GetFloat("playerFireForce");
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
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
