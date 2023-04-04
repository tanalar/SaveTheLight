using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform flameSpawner;
    [SerializeField] private Transform target;
    [SerializeField] private Transform randomTarget;

    private float fireForce;
    private float damage;
    private float minSize = 0.5f;
    private float currentSize;
    private float maxSize = 1;
    private float randomBurn = 0.0025f;
    private bool canFire = false;

    private float r;
    private float g;
    private float b;
    private float rGradientFrom = 0.9607843f;
    private float gGradientFrom = 0.6862745f;
    private float bGradientFrom = 0.09803922f;
    private float rGradientTo = 0.945098f;
    private float gGradientTo = 0.1529412f;
    private float bGradientTo = 0.06666667f;

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

    void Start()
    {
        SetValues();
        transform.localScale = new Vector3(minSize, minSize, minSize);
        currentSize = minSize;
        StartCoroutine(FlameDelay());
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, randomTarget.position, fireForce * Time.deltaTime);
    }

    public void StartFlame()
    {
        currentSize += randomBurn;
        r = rGradientTo - ((rGradientTo - rGradientFrom) / (maxSize - minSize) * (maxSize - currentSize));
        g = gGradientTo - ((gGradientTo - gGradientFrom) / (maxSize - minSize) * (maxSize - currentSize));
        b = bGradientTo - ((bGradientTo - bGradientFrom) / (maxSize - minSize) * (maxSize - currentSize));

        if (currentSize > maxSize)
        {
            currentSize = minSize;
            r = rGradientFrom;
            g = gGradientFrom;
            b = bGradientFrom;
            if (canFire)
            {
                transform.position = new Vector3(flameSpawner.position.x, flameSpawner.position.y, flameSpawner.position.z);
            }
            else
            {
                transform.position = new Vector3(-100, -100, -100);
            }
            randomTarget.position = new Vector3(target.position.x + Random.Range(-2.5f, 2.5f), target.position.y + Random.Range(-2.5f, 2.5f), target.position.z);

            randomBurn = Random.Range(0.005f, 0.0075f);
            maxSize = Random.Range(0.8f, 1.25f);
        }

        spriteRenderer.color = new Color(r, g, b);
        transform.localScale = new Vector3(currentSize, currentSize, currentSize);
    }
    private IEnumerator FlameDelay()
    {
        yield return new WaitForSeconds(0.0075f);
        StartFlame();
        StartCoroutine(FlameDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
        }
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
        fireForce = PlayerPrefs.GetFloat("flamethrowerFireForce");
        damage = PlayerPrefs.GetFloat("flamethrowerDamage");
    }
}
