using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefabs;
     GameObject spawnedGhosts;

    //GameObject spawnedGhostsPrefabs;

    //public SpriteRenderer ghostSprite;

    public ArrayList spawnedGhostsList;

    public float waitTime = 5;
    public Vector2 spawnPoint;

     public Transform ghostTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ghostSprite = GetComponent<SpriteRenderer>();

        spawnedGhosts = Instantiate(ghostPrefabs,Vector2.one, Quaternion.identity);

        //spawnedGhostsPrefabs = spawnedGhosts.GetComponent<GameObject>();
        ghostTransform = spawnedGhosts.GetComponent<Transform>();

        spawnedGhosts.transform.localScale = ghostTransform.localScale;

        //spawnedGhosts = spawnedGhostsPrefabs;

        //transform.position = spawnPoint;
        //spawnPoint.x = Random.Range(8.5f, -8.5f);
        //transform.position = spawnPoint;

    }

    // Update is called once per frame
    void Update()
    {
        //spawnedGhostsList.Add(spawnedGhosts);
        StartCoroutine(SpawnGhosts());

        Debug.Log("ghostTransform value:" + ghostTransform);
        Debug.Log("spawnedGhosts value:" + spawnedGhosts);
    }

    IEnumerator SpawnGhosts()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            //simulates ghosts approaching player by increasing their scale
            ghostTransform.localScale = Vector2.one * t;

            yield return null;
        }
        
        ////spawns ghost prefabs at a random x position (y constant) and no rotation
        //spawnedGhosts = Instantiate(ghostPrefabs,transform.position,Quaternion.identity);
        ////adds the spawned ghosts game obejcts to the arraylist of spawned ghosts
        //spawnedGhostsList.Add(spawnedGhosts);

        //yield return new WaitForSeconds(waitTime);

        //Instantiate(ghostPrefabs, transform.position, Quaternion.identity);

        //yield return null;
    }
}
