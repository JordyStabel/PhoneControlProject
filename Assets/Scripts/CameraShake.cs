using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public static CameraShake Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public IEnumerator Shake(float duration, float strengh)
    {
        Vector2 startingPos = this.transform.position;

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            transform.localPosition = Random.insideUnitSphere * strengh;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector2(0f, 0f);
    }
}