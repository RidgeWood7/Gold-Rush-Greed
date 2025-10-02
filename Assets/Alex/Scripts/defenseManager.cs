using UnityEngine;

public class defenseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public GameObject defensePlayer;
    public GameObject defenseHitbox;
    private Vector2 movementDirection;
    private Rigidbody2D rb2d;
    private float playerMovementSpeed = 4.0f;
    private int gold = 0;
    public GameObject bandit;
    private float spawnTime = 0.74f;
    private float spawnCD = 0;


    void Start()
    {
        rb2d = defensePlayer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnCD < spawnTime)
        {
            spawnCD += Time.deltaTime;
        }
        else
        {
            spawnBandit();
        }

            movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        rb2d.linearVelocity = movementDirection * playerMovementSpeed;
        
        

    }


    private void spawnBandit()
    {
        spawnCD = 0f;
        spawnTime = Mathf.Clamp(Random.Range(1.0f, 1.5f), 0.5f, 1.5f);
        Instantiate(bandit, new Vector3(-100.0f, 7.0f, 0.0f), Quaternion.identity);
    }


   



}
