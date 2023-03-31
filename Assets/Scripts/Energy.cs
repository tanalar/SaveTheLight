using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer energyTexture;
    private float speed = 3;
    private bool follow = false;
    private int value;
    private float a = 1;

    public static Action<int> onDestroy;

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    public void SetValues(LootData data)
    {
        value = data.value;
        transform.localScale = new Vector3(transform.localScale.x * data.scaleMultiplier, transform.localScale.y * data.scaleMultiplier, transform.localScale.z * data.scaleMultiplier);
        energyTexture.color = data.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            onDestroy?.Invoke(value);
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Magnet")
        {
            follow = true;
            player = GameObject.FindGameObjectWithTag("Player");
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
            Destroy(gameObject);
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
        yield return new WaitForSeconds(0.2f);
        a -= 0.01f;
        energyTexture.color = new Color(energyTexture.color.r, energyTexture.color.g, energyTexture.color.b, a);
        if(a <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(Destroy());
    }
}
