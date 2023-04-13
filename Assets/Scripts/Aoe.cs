using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aoe : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> wavesTextures;
    [SerializeField] private List<Enemy> list;
    private float wavesSpeedDelay = 0.025f;
    private float minRadius = 0;
    private float maxRadius = 1;
    private float maxTransparency = 0.2f;
    private float damage;
    private float slowdownMultiplier;
    private bool canSee = true;

    private void OnEnable()
    {
        Values.onSetValues += SetValues;
        Bonfire.onPlayerCanSee += CanSee;
    }
    private void OnDisable()
    {
        Values.onSetValues -= SetValues;
    }

    private void Start()
    {
        SetValues();
        StartCoroutine(Delay());
    }

    private void AoeAnimation()
    {
        for (int i = 0; i < wavesTextures.Count; i++)
        {
            float scale = wavesTextures[i].transform.localScale.x + 0.005f;
            wavesTextures[i].transform.localScale = new Vector3(scale, scale, scale);
            float a = maxTransparency - (maxTransparency - 0) / maxRadius * wavesTextures[i].transform.localScale.x;
            wavesTextures[i].color = new Color(wavesTextures[i].color.r, wavesTextures[i].color.g, wavesTextures[i].color.b, a);

            if (wavesTextures[i].transform.localScale.x >= maxRadius)
            {
                wavesTextures[i].transform.localScale = new Vector3(minRadius, minRadius, minRadius);
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(wavesSpeedDelay);
        AoeAnimation();
        DealDamage();
        StartCoroutine(Delay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            list.Add(enemyComponent);
        }
        if(collision.gameObject.TryGetComponent<EnemyFollow>(out EnemyFollow enemyFollowComponent))
        {
            enemyFollowComponent.SetAoeMultiplier(slowdownMultiplier);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            list.Remove(enemyComponent);
        }
        if (collision.gameObject.TryGetComponent<EnemyFollow>(out EnemyFollow enemyFollowComponent))
        {
            enemyFollowComponent.SetAoeMultiplier(1);
        }
    }

    private void DealDamage()
    {
        if (list.Count > 0 && canSee)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    list[i].TakeDamage(damage);
                }
            }
        }
    }

    private void SetValues()
    {
        damage = PlayerPrefs.GetFloat("aoeDamage");
        float prefsSlowdownMultiplier = PlayerPrefs.GetFloat("aoeSlowdownMultiplier");
        slowdownMultiplier = 1 - prefsSlowdownMultiplier;
        float radius = PlayerPrefs.GetFloat("aoeRadius");
        transform.localScale = new Vector3(radius, radius, radius);
    }

    private void CanSee(bool canSee)
    {
        this.canSee = canSee;
    }
}
