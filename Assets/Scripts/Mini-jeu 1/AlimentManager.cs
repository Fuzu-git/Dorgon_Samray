using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class AlimentManager : MonoBehaviour
{
    
    public static List<AlimentStruct> listOfAlim;

    public bool show;
    [Header("Propriétés de l'aliment à ajouter")]
    [SerializeField, ShowIf("show")] string nameOfNewAlim;
    [SerializeField, ShowIf("show")] Sprite spriteOfNewAlim;

    [Header("Les aliments")]
    [SerializeField, ShowIf("show")] GameObject legume;
    [SerializeField, ShowIf("show")] GameObject meat;
    [SerializeField, ShowIf("show")] GameObject fish;
    [SerializeField, ShowIf("show")] GameObject fruit;

    public bool ShowUI;
    [Header("UI relatif")]
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_scoring;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_errors;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_rate;
    [Button]
    public void AddAliment()
    {
        listOfAlim.Add(new AlimentStruct(nameOfNewAlim, spriteOfNewAlim));
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
        listOfAlim.Add(new AlimentStruct("Légume", legume.GetComponent<SpriteRenderer>().sprite));
        listOfAlim.Add(new AlimentStruct("Poisson", fish.GetComponent<SpriteRenderer>().sprite));
        listOfAlim.Add(new AlimentStruct("Viande", meat.GetComponent<SpriteRenderer>().sprite));
        listOfAlim.Add(new AlimentStruct("Fruit", fruit.GetComponent<SpriteRenderer>().sprite));
    }
    private void Awake()
    {
        IniatizeListOfAlim();     
    }

    private void Start()
    {
        Scoreboard.alimentToTake = listOfAlim[Random.Range(0, AlimentManager.listOfAlim.Count)].Name;
        Debug.Log("L'aliment à prendre est :" + Scoreboard.alimentToTake);
    }

    private void Update()
    {
        txt_errors.text = Scoreboard.errorCounter.ToString();
        txt_scoring.text = Scoreboard.totalScore.ToString();
        txt_rate.text = Scoreboard.rates.ToString();
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
