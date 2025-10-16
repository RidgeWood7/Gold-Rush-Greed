using UnityEngine;
using UnityEngine.Events;

public class defenseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Unity Event
    public UnityEvent addMoney;
    
    //Gold Pile
    public GameObject defenseHitbox;


    //Player Values
    private Vector2 movementDirection;
    public GameObject defensePlayer;
    private Rigidbody2D rb2d;
    private float playerMovementSpeed = 4.0f;

    //Bandit Prefab
    public GameObject bandit;

    //Adds and sets delay to when they spawn
    private float spawnTime = 0.74f;
    private float spawnCD = 0;

    //All used in monitoring how many bandits to spawn and left
    private int _banditsRemaining;
    private static int s_banditsPerRaid = 10;
    public int _endScene;


    void Start()
    {
        //All used in monitoring how many bandits to spawn and left
        _endScene = s_banditsPerRaid;   
        _banditsRemaining = s_banditsPerRaid;
        s_banditsPerRaid++;
        Debug.Log("Bandits Remaining: " + _banditsRemaining.ToString() + "\nBandits Per Raid: " + s_banditsPerRaid.ToString());

        //Sets Player RB2D
        rb2d = defensePlayer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //Checks if all the bandits have despawned
        if (_endScene < 1)
        {
            transitionScene();
        }

        //Spawns Bandits
        if (_banditsRemaining > 0)
        {
            //Sets a cooldown, limiting how often they spawn
            if (spawnCD < spawnTime)
            {
                spawnCD += Time.deltaTime;
            }
            else
            {
                spawnBandit();
            }

        }

        //Player movement
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        rb2d.linearVelocity = movementDirection * playerMovementSpeed;

    }



    private void transitionScene()
    {


        addMoney.Invoke();
        
        //This line toggles the defense parent 
        //transform.parent.gameObject.SetActive(false);

        //SWAP SCENES HERE 
    }

    private void spawnBandit()
    {
        //Resets Cooldown, Sets new time till a bandit spawns, and then spawns the bandit (the specific values have 0 meaning besides the fact its off screen)
        _banditsRemaining--;
        spawnCD = 0f;
        spawnTime = Random.Range(1.0f, 1.5f);
        Instantiate(bandit, new Vector3(-100.0f, 7.0f, 0.0f), Quaternion.identity);
    }


   



}
