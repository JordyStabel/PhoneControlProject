using UnityEngine;

public class InputPlayer : MonoBehaviour {

    protected Joystick joystick;

    //Sprites & Renderer
    private SpriteRenderer spriteRenderer;
    public Sprite heroIdleSide;
    public Sprite heroIdleFront;
    public Sprite heroIdleBack;

    private bool joyStickActive = false;
    private int direction = 0;

    // Use this for initialization
    void Start () {
        joystick = FindObjectOfType<Joystick>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        #region Player shooting input
        // If value is more than 0 then the joystick is in use
        if (joystick.Direction.x > 0 || joystick.Direction.y > 0)
            joyStickActive = true;
        else
            joyStickActive = false;

        // Bit weird because you don't want to shoot when using the joystick
        if ((joyStickActive && Input.touchCount == 2) || (!joyStickActive && Input.touchCount > 0))
        {
            Touch touch;

            if (joyStickActive)
                touch = Input.GetTouch(1);
            else
                touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            // Switch because I can...maybe do different stuff in the future
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Player.Instance.FireShot(direction, touchPos);
                    break;
                case TouchPhase.Moved:
                    //Player.Instance.FireShot(Physics2D.OverlapPoint(touchPos).transform);
                    break;
                case TouchPhase.Stationary:
                    //Player.Instance.FireShot(Physics2D.OverlapPoint(touchPos).transform);
                    break;
                case TouchPhase.Ended:
                    break;
            }
        }
        #endregion

        var rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity = new Vector2(joystick.Horizontal * 50, joystick.Vertical * 50);

        if (joystick.Direction.x >= 0.800f && joystick.Direction.x <= 0.880f && joystick.Direction.y <= 0.5f && joystick.Direction.y >= -0.5f)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = heroIdleSide;
            Player.Instance.ChangeHeadLightDirection(2);
            direction = 2;
        }
        else if (joystick.Direction.y >= 0.800f && joystick.Direction.y <= 0.880f && joystick.Direction.x <= 0.5f && joystick.Direction.x >= -0.5f)
        {
            spriteRenderer.sprite = heroIdleBack;
            Player.Instance.ChangeHeadLightDirection(0);
            direction = 0;
        }
        else if (joystick.Direction.y <= -0.800f && joystick.Direction.y >= -0.880f && joystick.Direction.x <= 0.5f && joystick.Direction.x >= -0.5f)
        {
            spriteRenderer.sprite = heroIdleFront;
            Player.Instance.ChangeHeadLightDirection(1);
            direction = 1;
        }
        else if (joystick.Direction.x <= -0.800f && joystick.Direction.x >= -0.880f && joystick.Direction.y <= 0.5f && joystick.Direction.y >= -0.5f)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = heroIdleSide;
            Player.Instance.ChangeHeadLightDirection(3);
            direction = 3;
        }
    }
}
