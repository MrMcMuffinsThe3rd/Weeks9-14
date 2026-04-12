using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSpawner : MonoBehaviour
{

    public GameObject invaderPrefabs;

    GameObject spawnedInvaders;

    public List<GameObject> spawnedInvadersList;

    public float waitTime = 5f;
    public Vector2 spawnPoint;

    public float timer;

    public Transform invaderTransform;

    public float t = 0;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnInvaders());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

    }

    IEnumerator SpawnInvaders()
    {
        //generates a spawn point based on invaderTransform
        invaderTransform.position = spawnPoint;
        spawnPoint.x = Random.Range(8.5f, -8.5f);
        spawnPoint.y = 6f;
        invaderTransform.position = spawnPoint;

        //spawns invader prefabs at a random x position (y constant) and no rotation
        spawnedInvaders = Instantiate(invaderPrefabs, spawnPoint, Quaternion.identity);

        //adds spawned invaders to a list
        spawnedInvadersList.Add(spawnedInvaders);

        StartCoroutine(MoveInvader());

        yield return null;
    }

    IEnumerator MoveInvader()
    {
        t = 0;

        speed = Random.Range(3, 5f);

        while (t < 4)
        {
            t += Time.deltaTime;

            //gets the transform of the spawned invaders and assignes it to invaderTransform variable
            invaderTransform = spawnedInvaders.GetComponent<Transform>();

            //sets the spawned invader position to invaderTransform variable position
            spawnedInvaders.transform.position = invaderTransform.position;

            //adds speed to y axis of the spawned invader's spawn point to move it downwards
            spawnPoint.y -= speed * Time.deltaTime; 

            //reassign transform.position to the invader prefab
            invaderTransform.position = spawnPoint;


            yield return null; //ghosts will take 8 seconds to reach the player before despawning
        }

        new WaitForSeconds(waitTime);

        StartCoroutine(DespawnInvaders());
        StartCoroutine(SpawnInvaders());

        yield return null;
    }

    IEnumerator DespawnInvaders()
    {
        for (int i = spawnedInvadersList.Count - 1; i >= 0; i--)
        {
            GameObject ghost = spawnedInvadersList[i];
            //despawn the ghost that has reached the player
            spawnedInvadersList.Remove(ghost);
            Destroy(ghost);
        }

        yield return null;
    }
}
