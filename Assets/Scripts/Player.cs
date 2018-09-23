using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public Transform player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Only one instance of this player
    }

    public Transform GetPlayerPosition()
    {
        return this.transform;
    }
}