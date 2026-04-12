using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefabs;

    GameObject spawnedGhosts;

    public List<GameObject> spawnedGhostsList;

    public float waitTime = 5f;
    public Vector2 spawnPoint;

     public Transform ghostTransform;

    public float timer;

    Coroutine spawnGhostsCoroutine;
    Coroutine moveGhostsCoroutine;

    bool spawnGhost = false;

    public float t = 0;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        StartCoroutine(SpawnGhosts());

        //StartCoroutine(MoveGhosts());


        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //StartCoroutine(StartSpawning());


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

            //generates a spawn point based on ghostTransform
            ghostTransform.position = spawnPoint;
            spawnPoint.x = Random.Range(8.5f, -8.5f);
            ghostTransform.position = spawnPoint;

            //spawns ghost prefabs at a random x position (y constant) and no rotation
            spawnedGhosts = Instantiate(ghostPrefabs, spawnPoint, Quaternion.identity);

            //adds spawned ghosts to a list
            spawnedGhostsList.Add(spawnedGhosts);



            StartCoroutine(MoveGhosts());

           //spawnGhost = true;

        yield return null;
    }

    IEnumerator MoveGhosts()
    {
        t = 0;

        speed = Random.Range(0.8f, 1f);

        while (t < 8)
        {
            t += Time.deltaTime;

            //make sure spawned ghost's scale is 1 before making them grow (appraoch player)
            spawnedGhosts.transform.localScale = Vector2.one;

            //gets the transform of the spawned ghosts and assignes it to ghostTransform variable
            ghostTransform = spawnedGhosts.GetComponent<Transform>();

            //sets the spawned ghosts scale to ghostTransform variable scale
            spawnedGhosts.transform.localScale = ghostTransform.localScale;

            //simulates ghosts approaching player by increasing their scale (using ghostTransform's scale)
            ghostTransform.localScale = Vector2.one * t * 0.5f;

            //reassign transform.scale to the ghost prefab
            spawnedGhosts.transform.localScale = ghostTransform.localScale;


            yield return null; //ghosts will take 8 seconds to reach the player before despawning
        }

        new WaitForSeconds(waitTime);

        StartCoroutine(DespawnGhosts());
        StartCoroutine(SpawnGhosts());

        yield return null;

    }

    IEnumerator DespawnGhosts()
    {
        for (int i = spawnedGhostsList.Count - 1; i >= 0; i--)
        {
            GameObject ghost = spawnedGhostsList[i];
            //despawn the ghost that has reached the player
            spawnedGhostsList.Remove(ghost);
            Destroy(ghost);
        }

        yield return null;
    }

    IEnumerator StartSpawning()
    {
        yield return spawnGhostsCoroutine = StartCoroutine(SpawnGhosts());
        yield return moveGhostsCoroutine = StartCoroutine(MoveGhosts());
        yield return new WaitForSeconds(waitTime);

        //it's also easier to just do the previous two lines but add yield return new WaitForSeconds() instead of this mess
    }
}
