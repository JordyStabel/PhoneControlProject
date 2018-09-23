using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    public float enemySpeed;
    public float stoppingDistance;
    public float followDistance;
    public float retreatDistance;
    public GameObject soundObject;

    [Header("Time between each shot")]
    public float startTimeBetweenShots;
    private float timeBetweenShots;
    public GameObject bullet;

    private bool inRange = false;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerTransform = Player.Instance.GetPlayerTransform();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        timeBetweenShots = startTimeBetweenShots;
    }

    void Update()
    {
        // Enemy is in range and moves towards the player
        if (Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance &&
            Vector2.Distance(transform.position, playerTransform.position) <= followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
            spriteRenderer.color = Color.red;
            inRange = true;
        }
        // Enemy is in range but doesn't retreat
        else if (Vector2.Distance(transform.position, playerTransform.position) < stoppingDistance &&
                Vector2.Distance(transform.position, playerTransform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            inRange = true;
        }
        // Enemy retreats from player, but is still in range
        else if (Vector2.Distance(transform.position, playerTransform.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, -enemySpeed * Time.deltaTime);
            spriteRenderer.color = Color.yellow;
            inRange = false;
        }
        // Enemy has no vision of player anymore
        else if (Vector2.Distance(transform.position, playerTransform.position) > followDistance)
        {
            spriteRenderer.color = Color.blue;
            inRange = false;
        }

        if (timeBetweenShots <= 0 && inRange)
        {
            FireShot();
            timeBetweenShots = startTimeBetweenShots;
        }
        else
            timeBetweenShots -= Time.deltaTime;
    }

    private void FireShot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);

        GameObject sound = (GameObject)Instantiate(soundObject, this.transform.position, this.transform.rotation);
        Destroy(sound, 2f);
    }
}