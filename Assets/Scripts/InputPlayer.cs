using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour {

    protected Joystick joystick;

    //Sprites & Renderer
    private SpriteRenderer spriteRenderer;
    public Sprite heroIdleSide;
    public Sprite heroIdleFront;
    public Sprite heroIdleBack;

    // Use this for initialization
    void Start () {
        joystick = FindObjectOfType<Joystick>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        var rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = new Vector2(joystick.Horizontal * 50, joystick.Vertical * 50);

        if (joystick.Direction.x >= 0.800f && joystick.Direction.x <= 0.880f && joystick.Direction.y <= 0.5f && joystick.Direction.y >= -0.5f)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = heroIdleSide;
            Debug.Log("Turning Right");
        }
        else if (joystick.Direction.y >= 0.800f && joystick.Direction.y <= 0.880f && joystick.Direction.x <= 0.5f && joystick.Direction.x >= -0.5f)
        {
            spriteRenderer.sprite = heroIdleBack;
            Debug.Log("Turning Top");
        }
        else if (joystick.Direction.y <= -0.800f && joystick.Direction.y >= -0.880f && joystick.Direction.x <= 0.5f && joystick.Direction.x >= -0.5f)
        {
            spriteRenderer.sprite = heroIdleFront;
            Debug.Log("Turning Back");
        }
        else if (joystick.Direction.x <= -0.800f && joystick.Direction.x >= -0.880f && joystick.Direction.y <= 0.5f && joystick.Direction.y >= -0.5f)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = heroIdleSide;
            Debug.Log("Turning Left");
        }
    }
}
