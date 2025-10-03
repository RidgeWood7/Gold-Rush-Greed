using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class banditScript : MonoBehaviour
{

    private defenseManager defenseScript;
    private float spawnLocation = 0.0f;
    private bool moving = true;
    private float speed = 1.5f;
    private static List<float> spawnLocations = new List<float> { -6, -4, -2, 0, 2, 4, 6 };
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defenseScript = GameObject.Find("DefenseManager").GetComponent<defenseManager>();
        gameObject.transform.position = chooseSpawn();

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!moving)
        {
            return;
        }
        if (collision.CompareTag("Hitbox"))
        {
            
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }

    private Vector3 chooseSpawn()
    {

        if (spawnLocations.Count <= 0)
        {
            
            for (float i = -6.0f; i < 7.0f; i += 2)
            {
                spawnLocations.Add(i);
                
            }

           
        }

        int index = Random.Range(0, spawnLocations.Count);
        spawnLocation = spawnLocations[index];

        spawnLocations.Remove(spawnLocation);
        


        return new Vector3(spawnLocation, 7, 0);
    }




}
