using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Pool))]

public class Sniper : MonoBehaviour
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private SpriteRenderer logo;
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
        pool.GetFreeElement(shotPoint.transform.position, shotPoint.transform.rotation);
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

    public void CanFire(bool canShoot)
    {
        canFire = canShoot;
    }

    private void SetValues()
    {
        fireRate = 0.25f - PlayerPrefs.GetFloat("sniperFireRate");
    }
}
