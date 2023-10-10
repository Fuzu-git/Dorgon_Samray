using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class Aliment : MonoBehaviour
{
    [SerializeField] GameObject despawn;
    public static List<AlimentStruct> listOfAlim;
    public static List<string> listOfname; // TEST

    [SerializeField] string alimentType;
    [SerializeField] string nameOfAlim;

    [Header("Propriétés de l'aliment à ajouter")]
    [SerializeField] string nameOfNewAlim;
    [SerializeField] Sprite spriteOfNewAlim;
    [Button]
    public void AddAliment()
    {
        listOfAlim.Add(new AlimentStruct(nameOfNewAlim, spriteOfNewAlim));
    }
    [Button]
    public void AddType() // TEST
    {
        listOfname.Add(new string(nameOfNewAlim));
    }
    [Button]
    public void NumberOfAliment()
    {
        if (listOfAlim == null)
        {
            Debug.Log("Pas d'aliments !");
            return;
        }
            
        Debug.Log(listOfAlim.Count);
    }
    [Button]
    public void IniatizeListOfAlim()
    {
        if (listOfAlim != null)
            return;
        listOfAlim = new List<AlimentStruct>();
        listOfAlim.Add(new AlimentStruct(nameOfAlim, gameObject.GetComponent<Sprite>()));
    }
    private void Awake()
    {
        IniatizeListOfAlim();     
    }
    private void Start()
    {

        if (listOfAlim != null)
        {
            Debug.Log(alimentType);
            this.alimentType = listOfAlim[Random.Range(0, listOfAlim.Count)].Name;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col = despawn.GetComponent<Collider2D>();
        Destroy(gameObject);        
    }
        
        
}

public class AlimentStruct
{
    public AlimentStruct(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
    }
    [SerializeField] string name;
    [SerializeField] Sprite sprite;

    public string Name { get => name;}
    public Sprite Sprite { get => sprite; }
}
