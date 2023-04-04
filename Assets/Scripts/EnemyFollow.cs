using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float speed;
    //private float distance;
    private float rageMultiplier = 1;
    private float aoeMultiplier = 1;

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

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (speed * rageMultiplier * aoeMultiplier) * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        float randomMultiplier = Random.Range(0.9f, 1.1f);
        this.speed = speed * PlayerPrefs.GetFloat("enemySpeedMultiplier") * randomMultiplier;
    }

    private void RageOn()
    {
        rageMultiplier = 1.25f;
    }
    private void RageOff()
    {
        rageMultiplier = 1;
    }

    public void SetAoeMultiplier(float multiplier)
    {
        aoeMultiplier = multiplier;
    }
}
