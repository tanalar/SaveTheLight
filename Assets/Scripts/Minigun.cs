using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class Minigun : MonoBehaviour
{
    [SerializeField] private List<Transform> shotPoints;
    //[SerializeField] private GameObject bulletPrefab;
    private float fireForce;
    private float fireRate;
    private bool canFire = false;
    private Pool pool;

    private void Start()
    {
        pool = GetComponent<Pool>();
        SetValues();
        //StartCoroutine(FireRate());
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
        int random = Random.Range(0, shotPoints.Count);
        PoolObject bullet = pool.GetFreeElement(shotPoints[random].transform.position, shotPoints[random].transform.rotation);
        //GameObject bullet = Instantiate(bulletPrefab, shotPoints[random].transform.position, shotPoints[random].transform.rotation);
        //bullet.GetComponent<Rigidbody2D>().AddForce(shotPoints[random].up * fireForce, ForceMode2D.Impulse);
    }

    private IEnumerator FireRate()
    {
        if (canFire)
        {
            Fire();
        }

        yield return new WaitForSeconds(fireRate);
        StartCoroutine(FireRate());
    }

    public void CanFire()
    {
        if (!canFire)
        {
            canFire = true;
            StartCoroutine(FireRate());
        }
    }
    public void CanNotFire()
    {
        if (canFire)
        {
            canFire = false;
            StopAllCoroutines();
        }
    }

    private void SetValues()
    {
        fireForce = PlayerPrefs.GetFloat("minigunFireForce");
        float prefsRate = PlayerPrefs.GetFloat("minigunFireRate");
        fireRate = 0.15f - prefsRate;
    }
}
