using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class banditScript : MonoBehaviour
{

    public UnityEvent removeGold;
    private defenseManager defenseScript;
    private float spawnLocation = 0.0f;

    //Checks if moving
    private bool moving = true;

    //Bandits Movement Speed
    private float speed = 1.5f;

    //Possible Spawn Locations For Bandits
    private static List<float> spawnLocations = new List<float> { -6, -4, -2, 0, 2, 4, 6 };
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //This finds the script of the player && gold pile
        defenseScript = GameObject.FindWithTag("DefenseManager").GetComponent<defenseManager>();

        //defenseScript = GameObject.Find("DefenseManager").GetComponent<defenseManager>();

        //Sets starting position
        gameObject.transform.position = chooseSpawn();

    }

    // Update is called once per frame
    void Update()
    {

        //Moves Bandit down so long as it hasn't collided with anything yet
        if (moving)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check to make sure other collisions havent happened
        if (!moving)
        {
            return;
        }

        //Bandit hits the gold pile
        if (collision.CompareTag("Hitbox"))
        {
            removeGold.Invoke();   
            Destroy(gameObject);

        }

        //Bandit hits the Player
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }

    private Vector3 chooseSpawn()
    {
        //Recreates the avaiable spawn locations
        if (spawnLocations.Count <= 0)
        {
            
            for (float i = -6.0f; i < 7.0f; i += 2)
            {
                spawnLocations.Add(i);
                
            }

           
        }


        //Chooses random spawn location from avaliable loctions than deletes it
        int index = Random.Range(0, spawnLocations.Count);
        spawnLocation = spawnLocations[index];
        spawnLocations.Remove(spawnLocation);
        

        //This returns the spawn position, with 7 being the Y level (How far up)
        return new Vector3(spawnLocation, 7, 0);
    }




}
