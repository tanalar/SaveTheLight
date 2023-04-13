using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float speed;
    private float rageMultiplier = 1;
    private float aoeMultiplier = 1;

    private void OnEnable()
    {
        Bonfire.onPlayerCanSee += Rage;
    }
    private void OnDisable()
    {
        Bonfire.onPlayerCanSee += Rage;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed * rageMultiplier * aoeMultiplier) * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        float randomMultiplier = Random.Range(0.9f, 1.1f);
        this.speed = speed * PlayerPrefs.GetFloat("enemySpeedMultiplier") * randomMultiplier;
    }

    private void Rage(bool canSee)
    {
        if (canSee == true)
        {
            rageMultiplier = 1;
        }
        if(canSee == false)
        {
            rageMultiplier = 1.5f;
        }
    }

    public void SetAoeMultiplier(float multiplier)
    {
        aoeMultiplier = multiplier;
    }
}
