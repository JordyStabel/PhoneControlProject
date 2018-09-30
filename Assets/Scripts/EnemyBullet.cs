using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float bulletSpeed;
    public int damage;
    public float cameraShakeMultiplier;
    public float camerShakeDuration;

    private Transform playerTransform;
    Vector2 target;

    public GameObject bulletDestroyEffect;
    public GameObject soundObject;

    void Start()
    {
        playerTransform = Player.Instance.GetPlayerTransform();
        target = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }

    void Update()
    {
        if (transform.position.x == target.x && transform.position.y == target.y)
            DestroyBullet();
        else
            transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.PlayerHit(damage, camerShakeDuration, cameraShakeMultiplier);
            DestroyBullet();
        }
        if (other.CompareTag("Wall"))
            DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
        GameObject sound = (GameObject)Instantiate(soundObject, this.transform.position, this.transform.rotation);
        Destroy(sound, 2f);
        GameObject temp = (GameObject)Instantiate(bulletDestroyEffect, transform.position, Quaternion.identity);
        Destroy(temp, 2f);
    }
}