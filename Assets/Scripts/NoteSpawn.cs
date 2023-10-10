using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteSpawn : MonoBehaviour
{
    public List<GameObject> spawnList = new();
    public List<GameObject> noteList = new();

    [SerializeField] private float delta = 2;
    [SerializeField] private float minDelta = 0.31f;
    private bool _canSpawn = true;

    private void Update()
    {
        if (_canSpawn)
        {
            StartCoroutine(RandomSpawnPoint());
        }
    }

    IEnumerator RandomSpawnPoint()
    {
        Transform selectedSpawnPoint = spawnList[Random.Range(0, spawnList.Count)].transform;
        Instantiate(noteList[Random.Range(0, noteList.Count)], selectedSpawnPoint);
        _canSpawn = false;
        yield return new WaitForSeconds(delta);
        if (delta >= minDelta)
        {
            delta -= 0.05f;
        }

        _canSpawn = true;
        Debug.Log("Delta Value " + delta);
    }
}