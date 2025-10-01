using UnityEngine;

public class defenseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public GameObject defensePlayer;
    public GameObject defenseHitbox;
    private Vector2 movementDirection;
    private Rigidbody2D rb2d;
    private float playerMovementSpeed = 4.0f;

    void Start()
    {
        rb2d = defensePlayer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        rb2d.linearVelocity = movementDirection * playerMovementSpeed;

    }
}
