using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sniper : MonoBehaviour
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer logo;
    private float fireForce = 50;
    private float fireRate = 0.25f;
    private bool canFire = false;
    private float minLoad = 0;
    private float currentLoad = 0;
    private float maxLoad = 1;

    private void Start()
    {
        SetValues();
        StartCoroutine(FireRate());
    }

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

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, shotPoint.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(shotPoint.up * fireForce, ForceMode2D.Impulse);
        currentLoad = minLoad;
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, currentLoad);
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireRate);
        if (currentLoad < maxLoad)
        {
            currentLoad += 0.05f;
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, currentLoad);
        }
        if (currentLoad >= maxLoad && canFire)
        {
            Fire();
        }
        StartCoroutine(FireRate());
    }

    public void CanFire()
    {
        canFire = true;
    }
    public void CanNotFire()
    {
        canFire = false;
    }

    private void SetValues()
    {
        //fireForce = PlayerPrefs.GetFloat("minigunFireForce");
        //float prefsRate = PlayerPrefs.GetFloat("minigunFireRate");
        //fireRate = 0.15f - prefsRate;
    }
}
