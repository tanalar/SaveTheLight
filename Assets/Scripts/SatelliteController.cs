using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] private FindClosestEnemy closestEnemy;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;

    private Quaternion q;
    private float dumping;
    private Vector3 velocity = Vector3.zero;
    private bool canSee = true;

    private void Start()
    {
        dumping = Random.Range(0.45f, 0.55f);
    }

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += CanSee;
        Bonfire.onPlayerCanNotSee += CanNotSee;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee -= CanSee;
        Bonfire.onPlayerCanNotSee -= CanNotSee;
    }

    private void Update()
    {
        //movement

        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, dumping);

        //rotation

        if (closestEnemy.closestEnemy != null && closestEnemy.enemies.Count > 0 && canSee)
        {
            Vector3 vectorToTarget = closestEnemy.closestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = q;
        }
        else
        {
            transform.rotation = q;
        }
    }

    private void CanSee()
    {
        canSee = true;
    }
    private void CanNotSee()
    {
        canSee = false;
    }
}
