using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private FindClosestEnemy closestEnemy;
    private float speed;
    private Vector2 move;
    private bool canSee = true;

    public static bool PointerDown = false;

    private void Start()
    {
        speed = PlayerPrefs.GetFloat("playerSpeed");
    }

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += CanSee;
        Bonfire.onPlayerCanNotSee += CanNotSee;
        Values.onUpgrade += SpeedUpgrade;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee -= CanSee;
        Bonfire.onPlayerCanNotSee -= CanNotSee;
        Values.onUpgrade -= SpeedUpgrade;
    }

    private void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

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
            float hAxis = move.x;
            float vAxis = move.y;
            float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, 0f, -zAxis);
        }
    }

    private void FixedUpdate()
    {
        if (PointerDown)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
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

    private void SpeedUpgrade()
    {
        speed = PlayerPrefs.GetFloat("playerSpeed");
    }
}
