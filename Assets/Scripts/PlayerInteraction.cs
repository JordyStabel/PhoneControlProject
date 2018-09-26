using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {

    public GameObject currentObject = null;
    public Text currentLevelText;
    private int currentLevel = 0;
 
    // Use this for initialization
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentLevelText.text = "Current Level: " + currentLevel.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            Debug.Log(collision.name);
            currentObject = collision.gameObject;
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Doors"))
        {
            if (currentObject != null)
            {
                Debug.Log("You opened the door with a key!");
                GameObject parentDoor = collision.transform.parent.gameObject;
                parentDoor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                parentDoor.GetComponent<BoxCollider2D>().enabled = false;
            }
            else if (currentObject == null)
            {
                Debug.Log("You can't open the door, you need a key!");
            }
        }
        else if(collision.CompareTag("Stairs"))
        {
            currentLevel += 1;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            Debug.Log("Now i should implement something in a game manager script");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
    }
}
