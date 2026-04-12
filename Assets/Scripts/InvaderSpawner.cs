using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSpawner : MonoBehaviour
{

    public GameObject invaderPrefabs;

    GameObject spawnedInvaders;

    public List<GameObject> spawnedInvadersList;

    public float waitTime = 2f;
    public Vector2 spawnPoint;

    public float timer;

    public Transform invaderTransform;

    public float t = 0;
    public float speed;

    Coroutine DespawnInvadersCoroutine;
    Coroutine moveInvadersCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pleaseStartIndependentInstancesOfCoroutineIBegU();

        StartCoroutine(StartSpawningInvaders());

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //if (moveInvadersCoroutine == null || timer > waitTime) //if it hasnt been assigned a value or its been 2 seconds
        //{
        //    startSpawningInvaders(); //spawn another invader 
        //    timer = 0; //reset the time
        //}

        //if (DespawnInvadersCoroutine != null && spawnedInvadersList.Count <= 0) //if an invader didnt despawned and there are no invaders in the list
        //{
        //    StopCoroutine(DespawnInvaders()); //stop despawning invaders
        //}

    }

    public void pleaseStartIndependentInstancesOfCoroutineIBegU()
    {
        StartCoroutine(MoveInvader());
    }

    IEnumerator StartSpawningInvaders()
    {

        timer = 0;

        while (spawnedInvadersList.Count <= 0 || timer > 2)
        {
            Debug.Log("spawningInvaders running");

            //generates a spawn point based on invaderTransform
            invaderTransform.position = spawnPoint;
            spawnPoint.x = Random.Range(8.5f, -8.5f);
            spawnPoint.y = 6f;
            invaderTransform.position = spawnPoint;

            //spawns invader prefabs at a random x position (y constant) and no rotation
            spawnedInvaders = Instantiate(invaderPrefabs, spawnPoint, Quaternion.identity);

            //adds spawned invaders to a list
            spawnedInvadersList.Add(spawnedInvaders);

            //gets the transform of the spawned invaders and assignes it to invaderTransform variable
            invaderTransform = spawnedInvaders.GetComponent<Transform>();

            //sets the spawned invader position to invaderTransform variable position
            spawnedInvaders.transform.position = invaderTransform.position;

            timer = 0;

            yield return new WaitForSeconds(waitTime);
        }

        Debug.Log("spawningInvaders done, moving...");

        StartCoroutine(MoveInvader());

    }

    IEnumerator SpawnInvaders()
    {


        yield return null;
    }

    IEnumerator MoveInvader()
    {
        t = 0;

        while (t < 4 || spawnedInvadersList.Count > 0)
        {
            for (int i = spawnedInvadersList.Count - 1; i >= 0; i--)
            {
                t += Time.deltaTime;

                speed = Random.Range(3f, 5f);

                GameObject moveInvader = spawnedInvadersList[i];
                Vector2 pos = moveInvader.transform.position;

                //adds speed to y axis of the spawned invader's spawn point to move it downwards
                pos.y -= speed * Time.deltaTime;

                //reassign transform.position to the invader prefab
                moveInvader.transform.position = pos;

                invaderTransform.position = pos; //to check position of spawned invaders

                StartCoroutine(DespawnInvaders());

                yield return null; //ghosts will take 8 seconds to reach the player before despawning
            }

            yield return null;
        }

    }

    IEnumerator DespawnInvaders()
    {
        while (invaderTransform.position.y < -6)
        {
            Debug.Log("Despawning....");

            for (int i = spawnedInvadersList.Count - 1; i > 0; i--)
            {
                GameObject invader = spawnedInvadersList[i];
                //despawn the invader that has reached the bottom of the screen
                spawnedInvadersList.Remove(invader);
                Destroy(invader);

                yield return null;
            }

            yield return null;

        }
    }

    IEnumerator MoveAndDespawn()
    {
        StartCoroutine(MoveInvader());
        new WaitForSeconds(waitTime);
        StartCoroutine(DespawnInvaders());

        yield return null;
    }
}
