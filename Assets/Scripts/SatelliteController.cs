using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] private FindClosestEnemy closestEnemy;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;

    private float dumping;
    private Vector3 velocity = Vector3.zero;
    private bool canSee = true;

    private void Start()
    {
        dumping = Random.Range(0.35f, 0.45f);
    }

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += CanSee;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee -= CanSee;
    }

    private void Update()
    {
        //movement
        Vector3 movePosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, dumping);

        //rotation
        if (closestEnemy.closestEnemy != null && closestEnemy.enemies.Count > 0 && canSee)
        {
            Vector3 vectorToTarget = closestEnemy.closestEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * maxRotationSpeed);
        }
        else
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * minRotationSpeed);
        }
    }

    private void CanSee(bool canSee)
    {
        this.canSee = canSee;
    }
}
