using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Rigidbody2D rb;
    private FindClosestEnemy closestEnemy;
    private float speed;
    private Vector2 move;
    private bool canSee = true;
    private bool canShoot = false;

    public static bool PointerDown = false;
    public static Action<bool> onShoot;


    private void Start()
    {
        SetValues();
        closestEnemy = GetComponent<FindClosestEnemy>();
    }

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += CanSee;
        Values.onSetValues += SetValues;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee -= CanSee;
        Values.onSetValues -= SetValues;
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
            transform.rotation = q;
            if(canShoot == false)
            {
                canShoot = true;
                onShoot?.Invoke(canShoot);
            }
        }
        else 
        {
            float hAxis = move.x;
            float vAxis = move.y;
            float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, 0f, -zAxis);
            if(canShoot == true)
            {
                canShoot = false;
                onShoot?.Invoke(canShoot);
            }
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

    private void CanSee(bool canSee)
    {
        this.canSee = canSee;
    }

    private void SetValues()
    {
        speed = PlayerPrefs.GetFloat("playerSpeed");
    }
}
