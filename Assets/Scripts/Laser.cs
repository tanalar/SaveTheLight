using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject laserTexture;
    [SerializeField] private SpriteRenderer laserSprite;

    private float fireRate;
    private float fireDuration;
    private float damage;
    private float minSize = 0;
    private float currentSize;
    private float maxSize = 0.1f;
    private bool canFire = false;
    private bool fire = true;

    private float r;
    private float g;
    private float b;
    private float rFrom = 1;
    private float gFrom = 0.2941177f;
    private float bFrom = 0.1686275f;
    private float rTo = 1;
    private float gTo = 0.254902f;
    private float bTo = 0.4235294f;

    private void OnEnable()
    {
        FindClosestEnemy.onNotEmpty += CanFire;
        FindClosestEnemy.onEmpty += CanNotFire;
        Values.onSetValues += SetValues;
    }
    private void OnDisable()
    {
        FindClosestEnemy.onNotEmpty -= CanFire;
        FindClosestEnemy.onEmpty -= CanNotFire;
        Values.onSetValues -= SetValues;
    }

    private void Start()
    {
        currentSize = maxSize;
        StartCoroutine(Delay());
    }

    private void Fire()
    {
        if (fire)
        {
            currentSize -= fireDuration;
            if (currentSize <= minSize)
            {
                fire = false;
                laserTexture.SetActive(false);
            }
        }
        if (!fire)
        {
            currentSize += fireRate;
            if (currentSize >= maxSize)
            {
                currentSize = maxSize;
                if (canFire)
                {
                    fire = true;
                    laserTexture.SetActive(true);
                }
            }
        }
        laserTexture.transform.localScale = new Vector3(currentSize, laserTexture.transform.localScale.y, laserTexture.transform.localScale.z);

        r = rFrom - ((rFrom - rTo) / maxSize * (maxSize - currentSize));
        g = gFrom - ((gFrom - gTo) / maxSize * (maxSize - currentSize));
        b = bFrom - ((bFrom - bTo) / maxSize * (maxSize - currentSize));
        laserSprite.color = new Color(r, g, b);
    }

    private IEnumerator Delay()
    {
        Fire();
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Delay());
    }

    public void CanFire()
    {
        canFire = true;
    }
    public void CanNotFire()
    {
        canFire = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
        }
    }

    private void SetValues()
    {
        fireRate = PlayerPrefs.GetFloat("laserFireRate");
        float prefsFireDuration = PlayerPrefs.GetFloat("laserFireDuration");
        fireDuration = 0.02f - prefsFireDuration;
        damage = PlayerPrefs.GetFloat("laserDamage");
    }
}
