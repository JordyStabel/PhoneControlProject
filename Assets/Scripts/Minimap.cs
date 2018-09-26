using UnityEngine;

public class Minimap : MonoBehaviour {

    public Transform player;

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;

        // Rotation
        //transform.rotation = Quaternion.Euler(0f, 0f, player.eulerAngles.z);
    }
}