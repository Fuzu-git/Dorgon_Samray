using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int score2 = 0;

    [SerializeField]
    private PowManager powManager;
    [SerializeField] private CuissonLevel cuissonLevel; 

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
                if (active && !noteSpawn.bonusIsActive && key == _note.noteColor)
                {
                    Destroy(note);
                    score2++;
                }

                if (active && noteSpawn.bonusIsActive)
                {
                    Destroy(note);
                    score2++;
                }
            }
        }
        
        if (score2 % 10 == 0 && score2 != 0)
        {
            powManager.enabled = true;
            cuissonLevel.enabled = true; 
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
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}