using UnityEngine;

public class Minimap : MonoBehaviour {

    public Transform player;

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;

        // Only if you want to have the minimap rotate with the player
        // transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}