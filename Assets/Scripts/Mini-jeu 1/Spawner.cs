using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;


public class Spawner : MonoBehaviour
{
    public bool show;
    [Header("GameObjects")]
    [SerializeField, ShowIf("show")] GameObject aliment;
    [SerializeField, ShowIf("show")] GameObject spawner;
    [SerializeField, ShowIf("show")] GameObject alimentClone;
    [SerializeField, ShowIf("show")] AlimentCollision alimentCollision;

    [SerializeField, ShowIf("show")] int numberOfSpawns;

    [Header("Booleans")]
    [SerializeField] bool inProgress;
    [SerializeField] public bool canSpawn;


    [Header("Largeur du spawner = largeur de l'�cran x le multiplier")]
    [SerializeField] float multiplier = 1;

    [Header("Angle de spawn / Taille des objets")]
    [SerializeField, ShowIf("show")]  float spawnAngleMax;
    [SerializeField, ShowIf("show")]  Vector2 spawnSizeMinMax;

    [Header("int anti malchance")]
    [SerializeField, ShowIf("show")] int countBetweenTwoCorrectAlimentMax = 4;
    [SerializeField, ShowIf("show")] public int countBetweenTwoCorrectAliment = 0;

    [Header("CD de spawn minimal et maximal")]
    [SerializeField] Vector2 secondsBetweenSpawnsMinMax;
    float nextSpawnTime;

    [Header("SpawnPosition")]
    Vector2 screenHalfSizeWorldUnits;
    [SerializeField] Vector2 spawnPositionModifier;


    [Header("Gizmos")]
    private Vector2 spawnPosition;
    [SerializeField] Vector2 spawnPositionMax;
    [SerializeField] Vector2 spawnPositionMin;

    public int CountBetweenTwoCorrectAlimentMax { get => countBetweenTwoCorrectAlimentMax; }

    void Start()
    {
        canSpawn = true;
        // Largeur du spawner ( largeur d'�cran x multiplicateur ) 
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * multiplier, Camera.main.orthographicSize);
        spawner.transform.localScale = new Vector3(1, spawner.transform.localScale.y * multiplier, 1);

    }

    void Update()
    {
        if(canSpawn)
        NormalSpawning();       
    }

    public void NormalSpawning()
    {
        // Assignation des param�tres des aliments lors du spawn
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x);


            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + spawnSize);
            spawnPositionMax = new Vector2( screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.y + spawnSize);
            spawnPositionMin = new Vector2( -screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.y + spawnSize);
            alimentClone = (GameObject)Instantiate(aliment, spawnPosition - spawnPositionModifier, Quaternion.Euler(Vector3.forward));

            numberOfSpawns++;
            alimentClone.transform.localScale = Vector2.one * spawnSize;
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(spawnPositionMin - spawnPositionModifier, spawnPositionMax - spawnPositionModifier);
    }
}
