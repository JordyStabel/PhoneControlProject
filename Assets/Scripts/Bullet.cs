using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    public float damage;

    private Transform playerTransform;
    Vector2 target;

    public GameObject bulletDestroyEffect;

    void Start()
    {
        playerTransform = Player.Instance.GetPlayerTransform();
        target = new Vector2(playerTransform.position.x, playerTransform.position.y);
    }

    void Update()
    {
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
            GameObject temp = (GameObject)Instantiate(bulletDestroyEffect, transform.position, Quaternion.identity);
            //Max duration of the animation
            Destroy(temp, 2f);
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player.Instance.Damage(damage);
            GameObject temp = (GameObject)Instantiate(bulletDestroyEffect, transform.position, Quaternion.identity);
            Destroy(temp, 2f);
        }
    }
}