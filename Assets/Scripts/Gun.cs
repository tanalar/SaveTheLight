using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private List<Transform> shotPoints;
    [SerializeField] private List<Transform> closedShotPoints;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private CircleCollider2D fireRange1;
    [SerializeField] private CircleCollider2D fireRange2;
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

    public void Fire()
    {
        if (canFire)
        {
            for (int i = 0; i < shotPoints.Count; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, shotPoints[i].transform.position, shotPoints[i].transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(shotPoints[i].up * fireForce, ForceMode2D.Impulse);
            }
        }
        StartCoroutine(FireRate());
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireRate);
        Fire();
    }

    public void GunUnlock()
    {
        shotPoints.Add(closedShotPoints[0]);
        shotPoints.Add(closedShotPoints[1]);
        closedShotPoints.RemoveAt(0);
        closedShotPoints.RemoveAt(0);
    }
    public void GunFireRate()
    {
        fireRate /= 1.2f;
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
        fireRange1.radius = PlayerPrefs.GetFloat("playerFireRange");
        fireRange2.radius = fireRange1.radius / 2;
        fireForce = PlayerPrefs.GetFloat("playerFireForce");
        float prefsRate = PlayerPrefs.GetFloat("playerFireRate");
        fireRate = 0.5f - prefsRate;
    }
}
