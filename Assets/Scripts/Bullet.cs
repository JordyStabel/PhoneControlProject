﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damage;
    public float cameraShakeMultiplier;
    public float camerShakeDuration;

    private Transform playerTransform;
    Vector2 target;

    public GameObject bulletDestroyEffect;
    public GameObject soundObject;

    public void SetTarget(Vector2 target)
    {
        this.target = target;
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
        if (other.CompareTag("Enemy"))
        {
            DestroyBullet();
            other.GetComponent<Enemy>().Destroy();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
        GameObject sound = (GameObject)Instantiate(soundObject, this.transform.position, this.transform.rotation);
        Destroy(sound, 3f);
        GameObject temp = (GameObject)Instantiate(bulletDestroyEffect, transform.position, Quaternion.identity);
        Destroy(temp, 2f);
    }
}