using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private Light2D Light;
    [SerializeField] private SpriteRenderer bonfireTexture;
    [SerializeField] private CircleCollider2D circleCollider;

    public static Action onEnemyVisible;
    public static Action onEnemyInvisible;
    public static Action onPlayerCanSee;
    public static Action onPlayerCanNotSee;

    private void OnEnable()
    {
        EnergyStorage.onAddLight += AddLight;
    }
    private void OnDisable()
    {
        EnergyStorage.onAddLight -= AddLight;
    }

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private void Fade()
    {
        if (Light.pointLightOuterRadius > 0)
        {
            Light.pointLightOuterRadius -= 0.04f;
            Light.pointLightInnerRadius = Light.pointLightOuterRadius / 10;
            circleCollider.radius= Light.pointLightOuterRadius;
            if (Light.pointLightOuterRadius <= 1)
            {
                bonfireTexture.transform.localScale = new Vector3(Light.pointLightOuterRadius, Light.pointLightOuterRadius, Light.pointLightOuterRadius);
            }
            if(Light.pointLightOuterRadius <= 0)
            {
                Light.pointLightOuterRadius = 0;
                StopCoroutine(Delay());
            }
        }
    }

    private IEnumerator Delay()
    {
        Fade();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Delay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.tag = "Visible";
            onEnemyVisible?.Invoke();
        }

        if(collision.gameObject.tag == "Player")
        {
            onPlayerCanSee?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.tag = "Invisible";
            onEnemyInvisible?.Invoke();
        }

        if (collision.gameObject.tag == "Player")
        {
            onPlayerCanNotSee?.Invoke();
        }
    }

    private void AddLight()
    {
        Light.pointLightOuterRadius += 2.5f / Light.pointLightOuterRadius;
    }
}
