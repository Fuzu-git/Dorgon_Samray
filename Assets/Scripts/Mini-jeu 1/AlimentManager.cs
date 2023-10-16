using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class AlimentManager : MonoBehaviour
{
    
    public List<AlimentStruct> listOfAlim;
    string laRecette;
    [SerializeField] float timeUntilNextRecette;

    [Header("Booleans")]
    [SerializeField,] bool nextGame;
    [SerializeField,] bool processing;
    private bool uiassign;
    public bool show;
    [Header("Propriétés de l'aliment à ajouter")]
    [SerializeField, ShowIf("show")] string nameOfNewAlim;
    [SerializeField, ShowIf("show")] Sprite spriteOfNewAlim;
    [SerializeField, ShowIf("show")] GameObject alimentCol;

    [SerializeField] Vector3 velocityLevel = new Vector3(10,15,20) ;

    [Header("Les aliments")]
    [SerializeField, ShowIf("show")] GameObject legume;
    [SerializeField, ShowIf("show")] GameObject meat;
    [SerializeField, ShowIf("show")] GameObject fish;
    [SerializeField, ShowIf("show")] GameObject fruit;
    [SerializeField, ShowIf("show")] GameObject gateau;
    [SerializeField, ShowIf("show")] GameObject glace;
    [SerializeField, ShowIf("show")] GameObject salade;
    [SerializeField, ShowIf("show")] GameObject sushi;
    [SerializeField, ShowIf("show")] GameObject huitre;
    //[SerializeField, ShowIf("show")] GameObject licorne;

    public bool ShowUI;
    [Header("UI relatif")]
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_scoring;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_errors;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_rate;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_level;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_success;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_laRecette;
    [SerializeField, ShowIf("ShowUI")] Image img_alimentTake;

    [Header("Refs à d'autres scripts")]
    [SerializeField, ShowIf("show")] NoteSpawn noteSpawn;
    [SerializeField, ShowIf("show")] Spawner spawner;
    [SerializeField] Sprite rouage;

    #region debug+addalim
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

    #endregion

    #region InitialisationDeRecette
    public void IniatizeListOfAlim()
    {
        if (listOfAlim != null)
            return;
        listOfAlim = new List<AlimentStruct>();

        int recette = Random.Range(1, 4);

        if(recette == 1)
        {
            laRecette = "Plat de résistance";
            listOfAlim.Add(new AlimentStruct("Légume", legume.GetComponent<SpriteRenderer>().sprite));
            listOfAlim.Add(new AlimentStruct("Poisson", fish.GetComponent<SpriteRenderer>().sprite));
            listOfAlim.Add(new AlimentStruct("Viande", meat.GetComponent<SpriteRenderer>().sprite));
        }
        if (recette == 2)
        {
            laRecette = "La mise en bouche";
            listOfAlim.Add(new AlimentStruct("Huitre", huitre.GetComponent<SpriteRenderer>().sprite));
            listOfAlim.Add(new AlimentStruct("Sushi", sushi.GetComponent<SpriteRenderer>().sprite));
            listOfAlim.Add(new AlimentStruct("salade", salade.GetComponent<SpriteRenderer>().sprite));
        }
        if (recette == 3)
        {
            laRecette = "Dessert dans l'désert";
            listOfAlim.Add(new AlimentStruct("Glace", glace.GetComponent<SpriteRenderer>().sprite));
            listOfAlim.Add(new AlimentStruct("Gateau", gateau.GetComponent<SpriteRenderer>().sprite));
            listOfAlim.Add(new AlimentStruct("Fruit", fruit.GetComponent<SpriteRenderer>().sprite));
        }

        txt_laRecette.text = laRecette;
    }
    #endregion
    private void Awake()
    {
        IniatizeListOfAlim();     
    }

    private void Start()
    {
        uiassign = true;
        AlimentToTake();
    }

    private void Update()
    {
        if (uiassign)
            UIAssignation();
        else
            return;

        if (!processing) 
        {
            ErrorsAndSuccess();
            Difficulty();
            
        }
        

        if (nextGame)
        {
            noteSpawn.enabled = true;
        }

        if (processing)
        {
            txt_laRecette.text = "En cours de traitement...";
            StartCoroutine(Processing());
        }

    }
    IEnumerator Processing()
    {
        spawner.canSpawn = false;
        listOfAlim = null;
        processing = false;
        uiassign = false;
        img_alimentTake.sprite = rouage;
        yield return new WaitForSeconds(timeUntilNextRecette);
        IniatizeListOfAlim() ;       
        AlimentToTake();
        spawner.canSpawn = true;
        uiassign = true;
    }
    private void AlimentToTake()
    {
        int goodOne = Random.Range(0,listOfAlim.Count);
        Scoreboard.alimentToTake = listOfAlim[goodOne].Name;
        Scoreboard.firstAliment = listOfAlim[goodOne].Name;
        Scoreboard.goodSprite = listOfAlim[goodOne].Sprite;
        Scoreboard.level = 1;
        Scoreboard.successToAchieve = 2;
        //Debug.Log("L'aliment à prendre est :" + Scoreboard.alimentToTake);
    }
    private void UIAssignation()
    {
        txt_errors.text = Scoreboard.errorCounter.ToString();
        txt_scoring.text = Scoreboard.totalScore.ToString();
        txt_rate.text = Scoreboard.rates.ToString();
        txt_level.text = Scoreboard.level.ToString();
        txt_success.text = Scoreboard.sucessCounter.ToString();
        img_alimentTake.sprite = Scoreboard.goodSprite;
    }

    private void ErrorsAndSuccess()
    {     
        if(Scoreboard.sucessCounter == Scoreboard.successToAchieve)
        {
            int goodOne = Random.Range(0, listOfAlim.Count);
            while (listOfAlim[goodOne].Name == Scoreboard.alimentToTake || listOfAlim[goodOne].Name == Scoreboard.firstAliment)
            {
                goodOne = Random.Range(0, listOfAlim.Count);
            }
            Scoreboard.alimentToTake = listOfAlim[goodOne].Name;
            Scoreboard.goodSprite = listOfAlim[goodOne].Sprite;
            Scoreboard.errorCounter = 0;
            Scoreboard.sucessCounter = 0;
            if(Scoreboard.level < 4)
            Scoreboard.level++;
        }

    }
    private void Difficulty()
    {
        if(Scoreboard.level == 1)
        {
            Scoreboard.successToAchieve = 2;
            alimentCol.GetComponent<AlimentCollision>().velocity = velocityLevel.x;
        }
        if (Scoreboard.level == 2)
        {
            Scoreboard.successToAchieve = 3;
            alimentCol.GetComponent<AlimentCollision>().velocity = velocityLevel.y;
        }
        if (Scoreboard.level == 3)
        {
            Scoreboard.successToAchieve = 5;
            alimentCol.GetComponent<AlimentCollision>().velocity = velocityLevel.z;
        }
        if(Scoreboard.level == 4)
        {
            Scoreboard.level = 0;
            if(!nextGame)
            nextGame = true;

            processing = true;
            alimentCol.GetComponent<AlimentCollision>().velocity = velocityLevel.x;
        }
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
