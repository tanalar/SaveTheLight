using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class Gun : MonoBehaviour
{
    [SerializeField] private List<Transform> shotPoints;
    [SerializeField] private List<Transform> closedShotPoints;
    [SerializeField] private CircleCollider2D fireRange1;
    [SerializeField] private CircleCollider2D fireRange2;
    private float fireRate;
    private bool canFire = false;
    private Pool pool;

    private void Start()
    {
        pool = GetComponent<Pool>();
        StartCoroutine(FireRate());
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

    public void Fire()
    {
        for (int i = 0; i < shotPoints.Count; i++)
        {
            pool.GetFreeElement(shotPoints[i].transform.position, shotPoints[i].transform.rotation);
        }
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
        fireRange1.radius = PlayerPrefs.GetFloat("playerFireRange");
        fireRange2.radius = fireRange1.radius / 2;
        fireRate = 0.5f - PlayerPrefs.GetFloat("playerFireRate");
    }
}
