using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteSpawn : MonoBehaviour
{
    public List<GameObject> spawnList = new();
    public List<GameObject> noteList = new();

    public List<AlimentStruct> ThisRecette = new(); 

    [SerializeField] private float delta = 2;
    [SerializeField] private float minDelta = 0.31f;
    private bool _canSpawn = true;

    public bool bonusIsActive;
    
    [SerializeField] private GameObject bonusNote; 

    private Note _note;
    [SerializeField] private BonusButton bonusButton;

    private void Start()
    {
        for (int i = 0; i < noteList.Count; i++)
        {
            noteList[i].GetComponent<SpriteRenderer>().sprite = ThisRecette[i].Sprite;
        }
        bonusNote.GetComponent<SpriteRenderer>().sprite = ThisRecette[2].Sprite;
    }

    private void Update()
    {
        if (_canSpawn)
        {
            StartCoroutine(RandomSpawnPoint());
        }

        if (bonusButton.cutterNumberNoteBonus == 0)
        {
            bonusIsActive = false; 
        }
    }

    IEnumerator RandomSpawnPoint()
    {
        Transform selectedSpawnPoint = spawnList[Random.Range(0, spawnList.Count)].transform;
        if (bonusIsActive && bonusButton.cutterNumberNoteBonus > 0) {
            Instantiate(bonusNote, selectedSpawnPoint);
            bonusButton.cutterNumberNoteBonus--;
        } else 
        {
            if (selectedSpawnPoint.gameObject == spawnList[0])
            {
                Instantiate(noteList[0], selectedSpawnPoint);
            } else if (selectedSpawnPoint.gameObject == spawnList[1])
            {
                Instantiate(noteList[1], selectedSpawnPoint);
            }
        } 
        _canSpawn = false;
        yield return new WaitForSeconds(delta);
        if (delta >= minDelta)
        {
            delta -= 0.05f;
        }
        _canSpawn = true;
    }
}