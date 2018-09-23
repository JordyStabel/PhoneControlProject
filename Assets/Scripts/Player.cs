using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public LayerMask fogLayer;
    public GameObject[] rayCastPos;

    public Transform player;
    public float startingHealth;
    private float health;
    private int direction = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Only one instance of this player

        health = startingHealth;
    }
    private void Update()
    {
        Ray ray = new Ray(rayCastPos[direction].transform.position, Camera.main.transform.position);
        RaycastHit hit;

        Debug.DrawRay(rayCastPos[direction].transform.position, (rayCastPos[direction].transform.position - Camera.main.transform.position), Color.green, 1, false);

        if (Physics.Raycast(ray, out hit, 2000, fogLayer, QueryTriggerInteraction.Collide))
        {
            GameObject temp = hit.transform.gameObject;
            temp.GetComponent<FogOfWar>().Hit(hit);
        }
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

    public void ChangeHeadLightDirection(int direction)
    {
        this.direction = direction;
    }
}