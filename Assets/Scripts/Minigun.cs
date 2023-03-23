using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    [SerializeField] private List<Transform> shotPoints;
    [SerializeField] private GameObject bulletPrefab;
    private float fireForce;
    private float fireRate;
    private bool canFire = false;

    private void Start()
    {
        Fire();
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
        if (canFire)
        {
            int random = Random.Range(0, shotPoints.Count);
            GameObject bullet = Instantiate(bulletPrefab, shotPoints[random].transform.position, shotPoints[random].transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(shotPoints[random].up * fireForce, ForceMode2D.Impulse);
        }
        StartCoroutine(FireRate());
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireRate);
        Fire();
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
        fireForce = PlayerPrefs.GetFloat("minigunFireForce");
        float prefsRate = PlayerPrefs.GetFloat("minigunFireRate");
        fireRate = 0.15f - prefsRate;
    }
}
