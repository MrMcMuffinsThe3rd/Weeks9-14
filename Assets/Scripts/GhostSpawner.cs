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

    int i;

    public float t = 0;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(MoveGhosts());
        StartCoroutine(SpawnGhosts());

        
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
        timer = 0;

        while (spawnedGhostsList.Count <= 0 || timer > 2)  
        {
            //generates a spawn point based on ghostTransform
            ghostTransform.position = spawnPoint;
            spawnPoint.x = Random.Range(8.5f, -8.5f);
            ghostTransform.position = spawnPoint;

            //spawns ghost prefabs at a random x position (y constant) and no rotation
            spawnedGhosts = Instantiate(ghostPrefabs, spawnPoint, Quaternion.identity);

            //adds spawned ghosts to a list
            spawnedGhostsList.Add(spawnedGhosts);

            speed = Random.Range(0.8f, 1f);

            //sets the spawned invader position to invaderTransform variable position
            spawnedGhosts.transform.localScale = ghostTransform.localScale;

            timer = 0;

            yield return new WaitForSeconds(waitTime);
        }

        StartCoroutine(MoveGhosts());
    }

    IEnumerator MoveGhosts()
    {
        while (t < 8 || spawnedGhostsList.Count > 0)
        {

            for (int i = spawnedGhostsList.Count - 1; i >= 0; i--)
            {
                t += Time.deltaTime;

                //creating an instance of ghost object
                GameObject scaleGhost = spawnedGhostsList[i];

                Vector2 size = scaleGhost.transform.localScale;

                size += Vector2.one * t * speed;

                scaleGhost.transform.localScale = size;

                //simulates ghosts approaching player by increasing their scale (using ghostTransform's scale)
                //scaleGhost.transform.localScale = Vector2.one * t * 0.5f;

                //reassign transform.scale to the ghost prefab transfrom
                //ghostTransform.localScale = scaleGhost.transform.localScale;

                ////make sure spawned ghost's scale is 1 before making them grow (appraoch player)
                //spawnedGhosts.transform.localScale = Vector2.one;

                ////gets the transform of the spawned ghosts and assignes it to ghostTransform variable
                //ghostTransform = spawnedGhosts.GetComponent<Transform>();

                ////sets the spawned ghosts scale to ghostTransform variable scale
                //spawnedGhosts.transform.localScale = ghostTransform.localScale;

                //ghostTransform.localScale = Vector2.one * t * 0.5f;

                ////reassign transform.scale to the ghost prefab
                //spawnedGhosts.transform.localScale = ghostTransform.localScale;

                yield return null; //ghosts will take 8 seconds to reach the player before despawning

            }

            t = 0;

            yield return null;
            yield return null;
        }

    }

    IEnumerator DespawnGhosts()
    {
        if (ghostTransform.transform.localScale.y > 5)
        {
            Debug.Log("Despawning....");


            //despawn the ghost that has reached the bottom of the screen

            GameObject invader = spawnedGhostsList[i];

            Debug.Log("invader:" + invader);

            spawnedGhostsList.Remove(invader);
            Destroy(spawnedGhostsList[i]);



        }

        yield return null;
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
