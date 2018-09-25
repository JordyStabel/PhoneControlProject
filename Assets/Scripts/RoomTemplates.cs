using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject fog;
    public int openingDirection;
    public GameObject closedRoom;

    //Door templates
    public GameObject topDoor;
    public GameObject bottomDoor;
    public GameObject rightDoor;
    public GameObject leftDoor;

    //Object templates
    public GameObject boss;
    public GameObject key;
    public GameObject stairs;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    private bool spawnedKey;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(waitTime <= 0 && spawnedBoss == false)
        {
			for (int i = 0; i < rooms.Count; i++)
            {
				if(i == rooms.Count-1)
                {
					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    Instantiate(stairs, rooms[i].transform.position, Quaternion.identity);
					spawnedBoss = true;

                    Vector2 bossRoomLocation = rooms[i].transform.position;
                    Quaternion bossRoomRotation = transform.rotation;
                    bossRoomRotation.z = 270;

                    if (openingDirection == 1)
                    {
                        bossRoomLocation.x = bossRoomLocation.x - 0.065f;
                        bossRoomLocation.y = bossRoomLocation.y - 39.33f;
                        Instantiate(bottomDoor, bossRoomLocation, Quaternion.identity);
                    }
                    else if (openingDirection == 2)
                    {
                        bossRoomLocation.x = bossRoomLocation.x - 0.065f;
                        bossRoomLocation.y = bossRoomLocation.y + 39.33f;
                        Instantiate(topDoor, bossRoomLocation, Quaternion.identity);
                    }
                    else if (openingDirection == 3)
                    {
                        bossRoomLocation.x = bossRoomLocation.x - 39.54f;
                        bossRoomLocation.y = bossRoomLocation.y - 0.005f;
                        Instantiate(leftDoor, bossRoomLocation, leftDoor.transform.rotation);
                    }
                    else if (openingDirection == 4)
                    {
                        bossRoomLocation.x = bossRoomLocation.x + 39.551f;
                        bossRoomLocation.y = bossRoomLocation.y + 0.122f;
                        Instantiate(rightDoor, bossRoomLocation, rightDoor.transform.rotation);
                    }
                }
			}
		}
        else if (waitTime <= 0 && spawnedKey == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 5)
                {
                    Instantiate(key, rooms[i].transform.position, Quaternion.identity);
                    spawnedKey = true;
                }
            }
        }
        else {
			waitTime -= Time.deltaTime;
		}
	}
}
