using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class Minigun : MonoBehaviour
{
    [SerializeField] private List<Transform> shotPoints;
    private float fireRate;
    private bool canFire = false;
    private Pool pool;

    private void Start()
    {
        pool = GetComponent<Pool>();
        SetValues();
    }

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

    private void Fire()
    {
        int random = Random.Range(0, shotPoints.Count);
        pool.GetFreeElement(shotPoints[random].transform.position, shotPoints[random].transform.rotation);
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

    public void CanFire(bool canShoot)
    {
        canFire = canShoot;
        if (canFire)
        {
            StartCoroutine(FireRate());
        }
        if (!canFire)
        {
            StopAllCoroutines();
        }
    }

    private void SetValues()
    {
        fireRate = 0.15f - PlayerPrefs.GetFloat("minigunFireRate");
    }
}
