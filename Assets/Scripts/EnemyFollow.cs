using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float speed;
    private float distance;
    private float rageMultiplier = 1;

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += RageOff;
        Bonfire.onPlayerCanNotSee += RageOn;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee -= RageOff;
        Bonfire.onPlayerCanNotSee -= RageOn;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //distance = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed * rageMultiplier) * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void RageOn()
    {
        rageMultiplier = 1.2f;
    }
    private void RageOff()
    {
        rageMultiplier = 1;
    }
}
