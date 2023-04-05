using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class Gun : MonoBehaviour
{
    [SerializeField] private List<Transform> shotPoints;
    [SerializeField] private List<Transform> closedShotPoints;
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private CircleCollider2D fireRange1;
    [SerializeField] private CircleCollider2D fireRange2;
    //private float fireForce;
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

    public void Fire()
    {
        for (int i = 0; i < shotPoints.Count; i++)
        {
            pool.GetFreeElement(shotPoints[i].transform.position, shotPoints[i].transform.rotation);
            //GameObject bullet = Instantiate(bulletPrefab, shotPoints[i].transform.position, shotPoints[i].transform.rotation);
            //bullet.GetComponent<Rigidbody2D>().AddForce(shotPoints[i].up * fireForce, ForceMode2D.Impulse);
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

    //public void GunUnlock()
    //{
    //    shotPoints.Add(closedShotPoints[0]);
    //    shotPoints.Add(closedShotPoints[1]);
    //    closedShotPoints.RemoveAt(0);
    //    closedShotPoints.RemoveAt(0);
    //}
    //public void GunFireRate()
    //{
    //    fireRate /= 1.2f;
    //}

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
        fireRange1.radius = PlayerPrefs.GetFloat("playerFireRange");
        fireRange2.radius = fireRange1.radius / 2;
        fireRate = 0.5f - PlayerPrefs.GetFloat("playerFireRate");
    }
}
