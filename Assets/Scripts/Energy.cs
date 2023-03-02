using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float speed = 3;
    private bool follow = false;
    private float distance;

    [SerializeField] private SpriteRenderer energyTexture;
    private LootData data;
    private float rFrom;
    private float gFrom;
    private float bFrom;
    private float rTo;
    private float gTo;
    private float bTo;
    private float r;
    private float g;
    private float b;
    public int currentColor = 2;
    private bool colorSwitcher = false;
    private int value;
    //private int zRotation = 0;
    //private float xScale = 0.4f;
    //private float yScale = 0.5f;
    //private bool sizeSwitcher = false;
    public static Action<int> onDestroy;

    public void SetValues(LootData data)
    {
        this.data = data;
        rFrom = data.rFrom;
        gFrom = data.gFrom;
        bFrom = data.bFrom;
        rTo = data.rTo;
        gTo = data.gTo;
        bTo = data.bTo;
        value = data.value;
        transform.localScale = new Vector3(transform.localScale.x * data.scaleMultiplier, transform.localScale.y * data.scaleMultiplier, transform.localScale.z * data.scaleMultiplier);

        StartCoroutine(Delay());
    }

    private void Animation()
    {
        if(colorSwitcher == false)
        {
            r = rFrom - ((rFrom - rTo) / 50 * (50 - currentColor));
            g = gFrom - ((gFrom - gTo) / 50 * (50 - currentColor));
            b = bFrom - ((bFrom - bTo) / 50 * (50 - currentColor));
            energyTexture.color = new Color(r, g, b);
            currentColor++;
            if (currentColor >= 50)
            {
                colorSwitcher = true;
            }
        }
        if (colorSwitcher == true)
        {
            r = rFrom - ((rFrom - rTo) / 50 * (50 - currentColor));
            g = gFrom - ((gFrom - gTo) / 50 * (50 - currentColor));
            b = bFrom - ((bFrom - bTo) / 50 * (50 - currentColor));
            energyTexture.color = new Color(r, g, b);
            currentColor--;
            if (currentColor <= 0)
            {
                colorSwitcher = false;
            }
        }

        //energyTexture.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, zRotation);
        //zRotation += 2;

        //energyTexture.transform.localScale = new Vector3(xScale, yScale, 0);
        //if(sizeSwitcher == false)
        //{
        //    xScale += 0.0025f;
        //    yScale -= 0.0025f;
        //    if (xScale >= 0.5f)
        //    {
        //        sizeSwitcher = true;
        //    }
        //}
        //if (sizeSwitcher == true)
        //{
        //    xScale -= 0.0025f;
        //    yScale += 0.0025f;
        //    if (xScale <= 0.4f)
        //    {
        //        sizeSwitcher = false;
        //    }
        //}
    }

    private IEnumerator Delay()
    {
        Animation();
        yield return new WaitForSeconds(0.025f);
        StartCoroutine(Delay());
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
    }

    private void Update()
    {
        if (follow && player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
