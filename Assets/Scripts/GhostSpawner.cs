using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefabs;

    GameObject spawnedGhosts;

    public ArrayList spawnedGhostsList;

    public float waitTime = 3;
    public Vector2 spawnPoint;

     public Transform ghostTransform;

    //Coroutine spawnGhostsCoroutine;
    //Coroutine spawnPositionCoroutine;

    public float t;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ghostSprite = GetComponent<SpriteRenderer>();

        //spawnedGhosts = Instantiate(ghostPrefabs,Vector2.one, Quaternion.identity);

        //spawnedGhostsPrefabs = spawnedGhosts.GetComponent<GameObject>();


        //spawnedGhosts = spawnedGhostsPrefabs;


        StartCoroutine(SpawnGhosts());



    }

    // Update is called once per frame
    void Update()
    {
        

        //spawnedGhostsList.Add(spawnedGhosts);

        //Debug.Log("ghostTransform value:" + ghostTransform);
        //Debug.Log("spawnedGhosts value:" + spawnedGhosts);
        //Debug.Log("transform.position.x value:" + spawnPoint.x);
    }

    public void StartSpawningGhosts()
    {

    }

    IEnumerator SpawnGhosts()
    {
        t = 0;

        //generates a spawn point based on ghostTransform
        ghostTransform.position = spawnPoint;
        spawnPoint.x = Random.Range(8.5f, -8.5f);
        ghostTransform.position = spawnPoint;

        //spawns ghost prefabs at a random x position (y constant) and no rotation
        spawnedGhosts = Instantiate(ghostPrefabs, spawnPoint, Quaternion.identity);

        //adds spawned ghosts to a list
        //spawnedGhostsList.Add(spawnedGhosts);

        Debug.Log("SpawnGhosts Coroutine started");

        while (t < 5)
        {
            t += Time.deltaTime;

            //make sure spawned ghost's scale is 1 before making them grow (appraoch player)
            spawnedGhosts.transform.localScale = Vector2.one;

            //gets the transform of the spawned ghosts and assignes it to ghostTransform variable
            ghostTransform = spawnedGhosts.GetComponent<Transform>();

            //sets the spawned ghosts scale to ghostTransform variable scale
            spawnedGhosts.transform.localScale = ghostTransform.localScale;

            //simulates ghosts approaching player by increasing their scale (using ghostTransform's scale)
            ghostTransform.localScale = Vector2.one * t;

            //reassign transform.scale to the ghost prefab
            spawnedGhosts.transform.localScale = ghostTransform.localScale;

            //run this coroutine and check if t=3, if yes, generate a new spawn point
            //StartCoroutine(SpawnPosition());

            Debug.Log("looping...");

            yield return null; //ghosts will take 10 seconds to reach the player before despawning
        }

        //despawn the ghost that has reached the player

    }

    IEnumerator SpawnPosition()
    {
        Debug.Log("Generating new spawn started");

        while (t == waitTime) //every 3 seconds, generate a new random position and set it to ghostTransform.position then run coroutine that spawns ghosts
        {
            ghostTransform.position = spawnPoint;
            spawnPoint.x = Random.Range(8.5f, -8.5f);
            ghostTransform.position = spawnPoint;

            //StartCoroutine(SpawnGhosts());

            yield return null;
        }

    }

    IEnumerator StartTheSpawning()
    {
        //yield return spawnGhostsCoroutine = StartCoroutine(SpawnGhosts());

        yield return new WaitForSeconds(waitTime);

        //yield return spawnPositionCoroutine = StartCoroutine(SpawnPosition());

        //it's also easier to just do the previous two lines but add yield return new WaitForSeconds() instead of this mess
    }
}
