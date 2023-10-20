using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Activator : MonoBehaviour
{
    bool active = false;
    GameObject note;
    Color old;
    private SpriteRenderer sr;

    public List<KeyCode> keyList = new();

    private Note _note;
    private BonusNote _bonusNote;
    [SerializeField] private NoteSpawn noteSpawn;

    
    [HideInInspector] public int score2 = 0;
    [SerializeField] private int numberToNextLevel; 

    [SerializeField]
    private PowManager powManager;
    [SerializeField] private CuissonLevel cuissonLevel;

    public static Action RecipeIsFinished; 

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        old = sr.color;
    }

    void Update()
    {
        foreach (KeyCode key in keyList)
        {
            
            if (Input.GetKeyDown(key))
            {
                StartCoroutine(Pressed());
                if (active && _note.isBonusNote)
                {
                    Destroy(note);
                    score2++;
                } else if (active && !_note.isBonusNote)
                {
                    if (key == _note.noteColor)
                    {
                        Destroy(note);
                        score2++;
                    }
                }

            }
        }
        if(numberToNextLevel != 0)
        {
            if (score2 % numberToNextLevel == 0 && score2 != 0)
            {
                powManager.enabled = true;
                cuissonLevel.enabled = true;
                RecipeIsFinished?.Invoke();
                score2 = 0;
                Scoreboard.recipeToDo += 1;
            }
        }
        else
        {
            Debug.Log("numberToNextLevel n'est pas initialisé");
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        active = true;
        if (col.gameObject.CompareTag("Note"))
        {
            note = col.gameObject;
            _note = note.GetComponent<Note>();
            _bonusNote = note.GetComponent<BonusNote>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active = false;
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0.5f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}