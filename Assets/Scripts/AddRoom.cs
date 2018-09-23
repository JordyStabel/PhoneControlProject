using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

    private RoomTemplates templates;

    // Use this for initialization
    void Start () {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
