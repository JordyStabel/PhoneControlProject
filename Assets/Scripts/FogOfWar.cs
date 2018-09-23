using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

    public GameObject fogPlane;
    private Transform player;
    public LayerMask fogLayer;
    public float fogRadius = 10f;

    public float fogArea { get { return fogRadius * fogRadius; } }

    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colors;

    void Start()
    {
        player = Player.Instance.GetPlayerTransform();
    }

    void Update()
    {
        player = Player.Instance.GetPlayerTransform();

        //Ray ray = new Ray(transform.position, player.position - transform.position);
        Ray ray = new Ray(transform.position, transform.position);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.green, 1, false);

        // Remove in case of removing the effect once player has been at a certain location
        //for (int i = 0; i < colors.Length; i++)
        //{
        //    colors[i] = Color.black;
        //}

        if (Physics.Raycast(ray, out hit, 2000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vector = fogPlane.transform.TransformPoint(vertices[i]);
                float distance = Vector3.SqrMagnitude(vector - hit.point);
                if (distance < fogArea)
                {
                    // Closer you get the more visible it becomes
                    float alpha = Mathf.Min(colors[i].a, (distance / fogArea));
                    colors[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }

    public void Initialize()
    {
        mesh = fogPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
    }

    void UpdateColor()
    {
        mesh.colors = colors;
    }

    public void Hit(RaycastHit hit)
    {
        Debug.Log("Fog hit");

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vector = fogPlane.transform.TransformPoint(vertices[i]);
            float distance = Vector3.SqrMagnitude(vector - hit.point);
            if (distance < fogArea)
            {
                // Closer you get the more visible it becomes
                float alpha = Mathf.Min(colors[i].a, (distance / fogArea));
                colors[i].a = alpha;
            }
        }
        UpdateColor();
    }
}