using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySize : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;
    private float max;
    private float min = 0.75f;
    private float current;
    private float to;
    private bool switcher = false;
    private float sizeChangerSpeed = 500;

    private void OnEnable()
    {
        Enemy.onHp += SizeChanger;
    }

    private void OnDisable()
    {
        Enemy.onHp -= SizeChanger;
    }

    private void Update()
    {
        if (switcher)
        {
            if (current > to)
            {
                current -= 0.01f * (Time.deltaTime * sizeChangerSpeed);
                if (current <= to)
                {
                    current = to;
                    switcher = false;
                }
            }
            transform.localScale = new Vector3(current, current, current);
        }
    }

    private void SizeChanger(float hp, float fullHp)
    {
        to = max - ((max - min) / fullHp * (fullHp - hp));
        switcher = true;
        //transform.localScale = new Vector3(to, to, to);
    }

    public void SetSize(float from, float to)
    {
        max = Random.Range(from, to);
        current = max;
        transform.localScale = new Vector3(max, max, max);
    }
}
