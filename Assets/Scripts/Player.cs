using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    [Header("General")]
    public LayerMask fogLayer;
    public GameObject[] rayCastPos;
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyHeart;
    public GameObject bullet;
    public GameObject soundObject;

    public GameObject gameOverUI;

    public Transform player;
    public int startingHealth;
    public int numberOfHearts;
    private int health;
    private int direction = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        // You can use the Playerprefs.GetInt to retrieve current health
        // Make sure you check the int since it doesn't get reset on quitting the game
        //Debug.Log(PlayerPrefs.GetInt("currentHealth", 5));
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

    public void PlayerHit(int damageAmount, float camerShakeDuration, float cameraShakeMultiplier)
    {
        Coroutine coroutine = StartCoroutine(CameraShake.Instance.Shake(camerShakeDuration, cameraShakeMultiplier));

        health -= damageAmount;
        UpdateHealth();

        if (health <= 0)
        {
            gameOverUI.SetActive(true);
            // Pause game time
            StopCoroutine(coroutine);
            Time.timeScale = 0f;
        }
    }

    public void ChangeHeadLightDirection(int direction)
    {
        this.direction = direction;
    }

    private void UpdateHealth()
    {
        if (health > numberOfHearts)
            health = numberOfHearts;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullheart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < numberOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
        //This is for potentially saving health through reloading scenes
        //PlayerPrefs.SetInt("currentHealth", health);
    }

    public void FireShot(int direction, Vector2 pos)
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().SetTarget(pos);

        //switch (direction)
        //{
        //    case 0:
        //        temp.GetComponent<Bullet>().SetTarget(transform.position + transform.forward);
        //        break;
        //    case 1:
        //        temp.GetComponent<Bullet>().SetTarget(transform.position - transform.forward);
        //        break;
        //    case 2:
        //        temp.GetComponent<Bullet>().SetTarget(transform.position + transform.right);
        //        break;
        //    case 3:
        //        temp.GetComponent<Bullet>().SetTarget(transform.position - transform.right);
        //        break;
        //}

        GameObject sound = (GameObject)Instantiate(soundObject, this.transform.position, this.transform.rotation);
        Destroy(sound, 2f);
    }
}