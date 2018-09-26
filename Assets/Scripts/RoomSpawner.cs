using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    private RoomTemplates templates;
    private int rand;
    private int randomEnemiesSpawn;
    public bool spawned = false;

    public float waitTime = 4f;

    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    public void spawnDungeon()
    {
      
    }



    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                Vector3 pos = transform.position;
                pos.z -= 5;
                GameObject temp = Instantiate(templates.fog, pos, templates.fog.transform.rotation);
                temp.GetComponent<FogOfWar>().fogPlane = temp;
                temp.GetComponent<FogOfWar>().Initialize();

                //Random number for enemy spawn based on currentLevel
                randomEnemiesSpawn = Random.Range(0, PlayerPrefs.GetInt("currentLevel", 0));
                Debug.Log(randomEnemiesSpawn);

                for (int i = 0; i < randomEnemiesSpawn; i++)
                {
                    //It will select a random int based on the amount of enemies added to the array
                    int randEnemiesArray = Random.Range(0, templates.enemies.Length);
                    Instantiate(templates.enemies[randEnemiesArray], transform.position, transform.rotation);
                }
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                Vector3 pos = transform.position;
                pos.z -= 5;
                GameObject temp = Instantiate(templates.fog, pos, templates.fog.transform.rotation);
                temp.GetComponent<FogOfWar>().fogPlane = temp;
                temp.GetComponent<FogOfWar>().Initialize();

                //Random number for enemy spawn based on currentLevel
                randomEnemiesSpawn = Random.Range(0, PlayerPrefs.GetInt("currentLevel", 0));
                Debug.Log(randomEnemiesSpawn);
                for (int i = 0; i < randomEnemiesSpawn; i++)
                {
                    //It will select a random int based on the amount of enemies added to the array
                    int randEnemiesArray = Random.Range(0, templates.enemies.Length);
                    Instantiate(templates.enemies[randEnemiesArray], transform.position, transform.rotation);
                }
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                Vector3 pos = transform.position;
                pos.z -= 5;
                GameObject temp = Instantiate(templates.fog, pos, templates.fog.transform.rotation);
                temp.GetComponent<FogOfWar>().fogPlane = temp;
                temp.GetComponent<FogOfWar>().Initialize();

                //Random number for enemy spawn based on currentLevel
                randomEnemiesSpawn = Random.Range(0, PlayerPrefs.GetInt("currentLevel", 0));
                Debug.Log(randomEnemiesSpawn);

                for (int i = 0; i < randomEnemiesSpawn; i++)
                {
                    //It will select a random int based on the amount of enemies added to the array
                    int randEnemiesArray = Random.Range(0, templates.enemies.Length);
                    Instantiate(templates.enemies[randEnemiesArray], transform.position, transform.rotation);
                }
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                Vector3 pos = transform.position;
                pos.z -= 5;
                GameObject temp = Instantiate(templates.fog, pos, templates.fog.transform.rotation);
                temp.GetComponent<FogOfWar>().fogPlane = temp;
                temp.GetComponent<FogOfWar>().Initialize();

                //Random number for enemy spawn based on currentLevel
                randomEnemiesSpawn = Random.Range(0, PlayerPrefs.GetInt("currentLevel", 0));
                Debug.Log(randomEnemiesSpawn);
                for (int i = 0; i < randomEnemiesSpawn; i++)
                {
                    //It will select a random int based on the amount of enemies added to the array
                    int randEnemiesArray = Random.Range(0, templates.enemies.Length);
                    Instantiate(templates.enemies[randEnemiesArray], transform.position, transform.rotation);
                }
            }
            spawned = true;
            templates.openingDirection = openingDirection;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false && other.GetComponent<RoomSpawner>().spawned == true)
            {

                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

    private void AddFog()
    {
        Vector3 pos = transform.position;
        pos.z -= 15;
        GameObject temp = Instantiate(templates.fog, pos, templates.fog.transform.rotation);
        temp.GetComponent<FogOfWar>().fogPlane = temp;
        temp.GetComponent<FogOfWar>().Initialize();
    }
}
