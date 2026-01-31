using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private float speed;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        Vector2 movement = new Vector2
        {
            x = 0,
            y = 0
        };

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }
        // gameObject.transform.Translate(movement * (speed * Time.deltaTime));
        Vector2 newPosition = rb2d.position + movement * (speed * Time.deltaTime);
        rb2d.MovePosition(newPosition);
    }
}
