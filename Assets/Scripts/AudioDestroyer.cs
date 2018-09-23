using UnityEngine;

public class AudioDestroyer : MonoBehaviour {

    private float soundLenght;

    void Start()
    {

        var Sound = this.GetComponent<AudioSource>();
        soundLenght = Sound.clip.length;
    }

    void Update()
    {
        soundLenght -= Time.deltaTime;
        if (soundLenght <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}