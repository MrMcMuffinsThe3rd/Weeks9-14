using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

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

    PlayerControllerInput playerController;

    int i;

    SpriteRenderer invaderSprite;

    bool WasInvaderShot = false;
    int howManyInvadersShot = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pleaseStartIndependentInstancesOfCoroutineIBegU();

        StartCoroutine(StartSpawningInvaders());

        //i = spawnedInvadersList.Count - 1;




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

        //Debug.Log("invaderTransform position" + invaderTransform.transform.position);

        if (spawnedInvadersList.Count > 0)
        {
            i = spawnedInvadersList.Count - 1;

        }
        else if (spawnedInvadersList.Count <= 0)
        {
            i = spawnedInvadersList.Count;
        }
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
            //Debug.Log("spawningInvaders running");

            //generates a spawn point based on invaderTransform
            invaderTransform.position = spawnPoint;
            spawnPoint.x = Random.Range(8.5f, -8.5f);
            spawnPoint.y = 6f;
            invaderTransform.position = spawnPoint;

            //spawns invader prefabs at a random x position (y constant) and no rotation
            spawnedInvaders = Instantiate(invaderPrefabs, spawnPoint, Quaternion.identity);

            invaderSprite = spawnedInvaders.GetComponent<SpriteRenderer>();

            //adds spawned invaders to a list
            spawnedInvadersList.Add(spawnedInvaders);

            speed = Random.Range(3f, 5f);

            //sets the spawned invader position to invaderTransform variable position
            spawnedInvaders.transform.position = invaderTransform.position;

            timer = 0;

            yield return new WaitForSeconds(waitTime);
        }

        //Debug.Log("spawningInvaders done, moving...");

        StartCoroutine(MoveInvader());

    }

    IEnumerator SpawnInvaders()
    {


        yield return null;
    }

    IEnumerator MoveInvader()
    {

        while (t < 4 || spawnedInvadersList.Count > 0)
        {

            for (int i = spawnedInvadersList.Count - 1; i >= 0; i--)
            {
                t += Time.deltaTime;

                GameObject moveInvader = spawnedInvadersList[i];
                Vector2 pos = moveInvader.transform.position;

                //adds speed to y axis of the spawned invader's spawn point to move it downwards
                pos.y -= speed * t;

                //reassign transform.position to the invader prefab
                moveInvader.transform.position = pos;

                invaderTransform.position = pos; //to check position of spawned invaders

                //Debug.Log("Speed: " + speed);



                yield return null; //ghosts will take 8 seconds to reach the player before despawning
            }

            t = 0;

            //Debug.Log("left moving loop, entering despawn");

            StartCoroutine(SpawnInvaders());

            //StartCoroutine(DespawnInvaders());

            yield return null;
        }

    }

    IEnumerator DespawnInvaders()
    {

        if (invaderTransform.transform.position.y < -6)
        {
            Debug.Log("Despawning....");

            //for (int i = spawnedInvadersList.Count - 1; i >= 0; i--)
            //{
            //despawn the invader that has reached the bottom of the screen

                GameObject invader = spawnedInvadersList[i];

                Debug.Log("invader:" + invader);

                spawnedInvadersList.Remove(invader);
            Destroy(spawnedInvadersList[i]);

            //    yield return null;
            //}

           

        }

        yield return null;

        StartCoroutine(SpawnInvaders());
    }


    public void OnPoint(InputAction.CallbackContext context)
    {
        //playerController.cursor.transform.localPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

        //playerController.cursor.transform.position = transform.position;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());

        float distance = Vector2.Distance(cursorPos, invaderTransform.position);

        if ((distance < 0.1f) || invaderTransform.position.y < -6)
        {
           
                Debug.Log("Despawning....");

            //Debug.Log("playerController.cursor.transform.position: " + playerController.cursor.position);
            //Debug.Log("invaderSprite");


            //for (int i = spawnedInvadersList.Count - 1; i >= 0; i--)
            //{
            //despawn the invader that has reached the bottom of the screen

            GameObject invader = spawnedInvadersList[i];

                Debug.Log("invader:" + invader);

                spawnedInvadersList.Remove(invader);
                Destroy(invader);

                //    yield return null;
                //}



           
        }
    }
}
