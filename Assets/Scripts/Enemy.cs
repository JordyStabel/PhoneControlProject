using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    public float enemySpeed;
    public float stoppingDistance;
    public float followDistance;
    public float retreatDistance;
    public GameObject shootSound;

    public GameObject destroySound;
    public GameObject destroyEffect;

    public float camerShakeDuration;
    public float cameraShakeMultiplier;

    [Header("Time between each shot")]
    public float startTimeBetweenShots;
    private float timeBetweenShots;
    public GameObject bullet;

    private bool inRange = false;
    private bool inVision = false;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        timeBetweenShots = startTimeBetweenShots;

        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        playerTransform = Player.Instance.GetPlayerTransform();

        CheckVision();

        inRange = Vector2.Distance(transform.position, playerTransform.position) <= followDistance;

        // Enemy is in range and moves towards the player
        if (Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance && inRange && inVision)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
            spriteRenderer.color = Color.red;
        }
        // Enemy is in range but doesn't retreat
        else if (Vector2.Distance(transform.position, playerTransform.position) < stoppingDistance &&
                Vector2.Distance(transform.position, playerTransform.position) > retreatDistance && inVision && inRange)
            transform.position = this.transform.position;

        // Enemy retreats from player, but is still in range
        else if (Vector2.Distance(transform.position, playerTransform.position) < retreatDistance && inVision && inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, -enemySpeed * Time.deltaTime);
            spriteRenderer.color = Color.yellow;
        }
        // Enemy has no vision of player anymore
        else if (!inRange && !inVision)
        {
            spriteRenderer.color = Color.blue;
            transform.position = this.transform.position;
        }

        if (timeBetweenShots <= 0 && inRange && inVision)
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

        GameObject sound = (GameObject)Instantiate(shootSound, this.transform.position, this.transform.rotation);
        Destroy(sound, 2f);
    }

    public void Destroy()
    {
        StartCoroutine(CameraShake.Instance.Shake(camerShakeDuration, cameraShakeMultiplier));
        GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        GameObject sound = Instantiate(destroySound, transform.position, Quaternion.identity);
        Destroy(sound, 3f);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, (camerShakeDuration + 1f));
    }

    private void CheckVision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (playerTransform.position - this.transform.position));

        if (hit.collider.CompareTag("Wall"))
            inVision = false;
        else
            inVision = true;
    }
}