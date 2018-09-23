using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public Transform player;
    public float startingHealth;
    private float health;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Only one instance of this player

        health = startingHealth;
    }

    public Transform GetPlayerTransform()
    {
        return this.transform;
    }

    public void Damage(float amount)
    {
        health -= amount;

        if (health <= 0)
            Debug.Log("You ded boi!");
    }
}