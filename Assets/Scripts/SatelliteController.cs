using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] private FindClosestEnemy closestEnemy;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float dumping;
    private Vector3 velocity = Vector3.zero;
    private bool canSee = true;

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

    private void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, dumping);
    }

    private void Update()
    {
        //rotation

        if (closestEnemy.closestEnemy != null && closestEnemy.enemies.Count > 0 && canSee)
        {
            Vector3 vectorToTarget = closestEnemy.closestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
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
