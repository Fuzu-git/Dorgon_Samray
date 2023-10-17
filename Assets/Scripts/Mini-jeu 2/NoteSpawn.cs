using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NoteSpawn : MonoBehaviour
{
    public List<GameObject> spawnList = new();
    public List<GameObject> noteList = new();

    public List<AlimentStruct> ThisRecipe = new();
    public List<List<AlimentStruct>> ListOfRecipes = new();
    private List<AlimentStruct> _recipe1 = new();
    private List<AlimentStruct> _recipe2 = new();
    private List<AlimentStruct> _recipe3 = new();
    public bool once = false; 
    
    [HideInInspector] public int numberOfRecipe = 0;

    [SerializeField] private float delta = 2;
    [SerializeField] private float minDelta = 0.31f;
    private bool _canSpawn = true;

    public bool bonusIsActive;
    [SerializeField] private BonusButton bonusButton;
    [SerializeField] private GameObject bonusNote; 

    private Note _note;

    private void Start()
    {
        switch (numberOfRecipe)
        {
            case 1:
                _recipe1 = ThisRecipe;
                break; 
            case 2:
                _recipe2 = ThisRecipe;
                break;
            case 3:
                _recipe3 = ThisRecipe;
                break; 
        }
    }
    
    private void Update()
    {
        if (once)
        { 
            RecipeList();
        }
        
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

    private void RecipeList()
    {
        for (int i = 0; i < noteList.Count; i++)
        {
            switch (numberOfRecipe)
            {
                case 1: 
                    noteList[i].GetComponent<SpriteRenderer>().sprite = _recipe1[i].Sprite;
                    break;
                case 2:
                    noteList[i].GetComponent<SpriteRenderer>().sprite = _recipe2[i].Sprite;
                    break; 
                case 3:
                    noteList[i].GetComponent<SpriteRenderer>().sprite = _recipe3[i].Sprite;
                    break; 
                    
            }
        }
        bonusNote.GetComponent<SpriteRenderer>().sprite = ThisRecipe[2].Sprite;
    }
}