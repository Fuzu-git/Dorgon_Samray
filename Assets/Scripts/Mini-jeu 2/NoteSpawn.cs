using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NoteSpawn : MonoBehaviour
{
    public List<GameObject> spawnList = new();
    public List<GameObject> noteList = new();

    public List<List<AlimentStruct>> ListOfRecipes = new();
    private bool _hasStarted = false;  

    [SerializeField] private float delta = 2;
    [SerializeField] private float minDelta = 0.31f;
    
    private bool _canSpawn = true;

    public bool bonusIsActive;
    [SerializeField] private BonusButton bonusButton;
    [SerializeField] private GameObject bonusNote; 

    private Note _note;

    private void Awake()
    {
        Activator.RecipeIsFinished += NextRecipe;
    }

    private void NextRecipe()
    {
        ListOfRecipes.RemoveAt(0);
    }


    private void Update()
    {
        if (ListOfRecipes.Count == 0 && _hasStarted == true) //Si le jeu a démarré et pas de recette 
        {
            //on arrête le jeu
            _hasStarted = false; 
            StopCoroutine(RandomSpawnPoint());
            _canSpawn = true; 
        } else if (ListOfRecipes.Count > 0 && !_hasStarted) //si le jeu n'a pas démarré et il y a au moins une recette
        {
            //on lance le jeu
            _hasStarted = true;
        }
        
        if (_canSpawn && _hasStarted)
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
        List<AlimentStruct> recipe = ListOfRecipes[0];
        
        Transform selectedSpawnPoint = spawnList[Random.Range(0, spawnList.Count)].transform;
        if (bonusIsActive && bonusButton.cutterNumberNoteBonus > 0) {
            var thisAliment = Instantiate(bonusNote, selectedSpawnPoint);
            thisAliment.GetComponent<SpriteRenderer>().sprite = recipe[2].Sprite;
            bonusButton.cutterNumberNoteBonus--;
        } else 
        {
            if (selectedSpawnPoint.gameObject == spawnList[0])
            {
                var thisAliment = Instantiate(noteList[0], selectedSpawnPoint);
                thisAliment.GetComponent<SpriteRenderer>().sprite = recipe[0].Sprite;
            } else if (selectedSpawnPoint.gameObject == spawnList[1])
            {
                var thisAliment = Instantiate(noteList[1], selectedSpawnPoint);
                thisAliment.GetComponent<SpriteRenderer>().sprite = recipe[1].Sprite;
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