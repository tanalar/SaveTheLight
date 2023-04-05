using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class Sniper : MonoBehaviour
{
    [SerializeField] private Transform shotPoint;
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer logo;
    //private float fireForce = 50;
    [SerializeField]private float fireRate = 0.25f;
    private bool canFire = false;
    private float minLoad = 0;
    private float currentLoad = 0;
    private float maxLoad = 1;
    private Pool pool;

    private void Start()
    {
        pool = GetComponent<Pool>();
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
        pool.GetFreeElement(shotPoint.transform.position, shotPoint.transform.rotation);
        //GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, shotPoint.transform.rotation);
        //bullet.GetComponent<Rigidbody2D>().AddForce(shotPoint.up * fireForce, ForceMode2D.Impulse);
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
        if (!canFire)
        {
            canFire = true;
        }
    }
    public void CanNotFire()
    {
        if (canFire)
        {
            canFire = false;
        }
    }

    private void SetValues()
    {
        //fireForce = PlayerPrefs.GetFloat("minigunFireForce");
        fireRate = 0.25f - PlayerPrefs.GetFloat("sniperFireRate");
    }
}
