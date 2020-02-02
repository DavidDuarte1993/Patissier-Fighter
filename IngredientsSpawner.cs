using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] ingredients=null;
    [SerializeField]
    float spawningTime = 20;
    float spawningCounter = 0;
    [SerializeField]
    public Transform[] spawnpoint = null;
    int rand ;
    int r=0;

void Start()
    {
        spawningCounter = spawningTime;
        spawnpoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        spawningCounter -= Time.deltaTime;
        if (spawningCounter <= 0)
        {
            spawningCounter = spawningTime;
            r++;
            r%=spawnpoint.Length;
             rand = Random.Range(0, ingredients.Length);
             
           
            Instantiate(ingredients[rand], spawnpoint[r].position, Quaternion.identity);
        }
    }
}
