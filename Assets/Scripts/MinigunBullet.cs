using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunBullet : MonoBehaviour
{
    private float damage;

    private void Start()
    {
        damage = PlayerPrefs.GetFloat("minigunDamage");
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
