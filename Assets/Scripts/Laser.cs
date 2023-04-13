using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject laserTexture;
    [SerializeField] private SpriteRenderer laserSprite;
    [SerializeField] private List<Enemy> list;

    private float fireRate;
    private float fireDuration;
    private float damage;
    private float minSize = 0;
    private float currentSize;
    private float maxSize = 0.075f;
    private bool canFire = false;
    private bool fire = true;

    private float r;
    private float g;
    private float b;
    private float rFrom = 0.8823529f;
    private float gFrom = 0f;
    private float bFrom = 1f;
    private float rTo = 0.4980392f;
    private float gTo = 0f;
    private float bTo = 1f;

    private void OnEnable()
    {
        Values.onSetValues += SetValues;
        PlayerController.onShoot += CanFire;
    }
    private void OnDisable()
    {
        Values.onSetValues -= SetValues;
        PlayerController.onShoot -= CanFire;
    }

    private void Start()
    {
        SetValues();
        currentSize = maxSize;
        StartCoroutine(Delay());
    }

    private void Fire()
    {
        if (fire)
        {
            if(list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null)
                    {
                        list[i].TakeDamage(damage);
                    }
                }
            }

            currentSize -= fireDuration;
            if (currentSize <= minSize)
            {
                fire = false;
                laserTexture.SetActive(false);
                list.Clear();
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

    public void CanFire(bool canShoot)
    {
        canFire = canShoot;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            list.Add(enemyComponent);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            list.Remove(enemyComponent);
        }
    }

    private void SetValues()
    {
        fireRate = PlayerPrefs.GetFloat("laserFireRate");
        fireDuration = 0.02f - PlayerPrefs.GetFloat("laserFireDuration");
        damage = PlayerPrefs.GetFloat("laserDamage");
    }
}
