using UnityEngine;

public class MinimapTrigger : MonoBehaviour {

    [Header("The index of layer")]
    public int visible;
    public int invisible;
    public GameObject room;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && room.layer.Equals(invisible))
            ChangeLayers(room, visible);
    }

    public static void ChangeLayers(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }
}