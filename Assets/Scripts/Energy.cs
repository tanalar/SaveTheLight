using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]

public class Energy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer energyTexture;
    private float speed = 3;
    private bool follow = false;
    private int value;
    private float a;
    private PoolObject poolObject;

    public static Action<int> onDestroy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        poolObject = GetComponent<PoolObject>();
    }

    private void OnEnable()
    {
        StartCoroutine(Destroy());
    }

    public void SetValues(LootData data)
    {
        value = data.value;
        transform.localScale = new Vector3(data.scale, data.scale, data.scale);
        energyTexture.color = data.color;
        a = 1;
        energyTexture.color = new Color(energyTexture.color.r, energyTexture.color.g, energyTexture.color.b, a);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            onDestroy?.Invoke(value);
            poolObject.ReturnToPool();
        }
        if(collision.gameObject.tag == "Magnet")
        {
            follow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Magnet")
        {
            follow = false;
        }
        if(collision.gameObject.tag == "Bonfire")
        {
            poolObject.ReturnToPool();
        }
    }

    private void Update()
    {
        if (follow && player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.15f);
        a -= 0.01f;
        energyTexture.color = new Color(energyTexture.color.r, energyTexture.color.g, energyTexture.color.b, a);
        if(a <= 0)
        {
            StopAllCoroutines();
            poolObject.ReturnToPool();
        }
        if(gameObject.activeInHierarchy == true)
        {
            StartCoroutine(Destroy());
        }
    }
}
