using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public GameObject currentObject = null;

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
            Debug.Log("Now i should implement something in a game manager script");
        }
    }
}
