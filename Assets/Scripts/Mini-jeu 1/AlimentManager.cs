using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class AlimentManager : MonoBehaviour
{
    public bool show;
    public bool ShowUI;
    
    public List<AlimentStruct> listOfAlim;
    string laRecette;
    [SerializeField] float timeUntilNextRecette;

    [Header("Booleans")]
    [SerializeField] bool nextGame;
    [SerializeField] bool processing;
    private bool uiassign;
    [SerializeField] int nbOfSucc1;
    [SerializeField] int nbOfSucc2;
    [SerializeField] int nbOfSucc3;
    [Header("int")]

    [Header("Propri�t�s de l'aliment � ajouter")]
    [SerializeField, ShowIf("show")] string nameOfNewAlim;
    [SerializeField, ShowIf("show")] Sprite spriteOfNewAlim;
    [SerializeField, ShowIf("show")] GameObject alimentCol;

    [SerializeField] Vector3 velocityLevel = new Vector3(10,15,20) ;

    [Header("Les aliments")]
    [SerializeField, ShowIf("show")] Sprite licorne;
    [SerializeField, ShowIf("show")] Sprite serpent;
    [SerializeField, ShowIf("show")] Sprite pomme;
    [SerializeField, ShowIf("show")] Sprite basilic;
    [SerializeField, ShowIf("show")] Sprite champignon;
    [SerializeField, ShowIf("show")] Sprite dragon;
    [SerializeField, ShowIf("show")] Sprite gobelin;
    [SerializeField, ShowIf("show")] Sprite haricots;
    [SerializeField, ShowIf("show")] Sprite scarab�e;
    //[SerializeField, ShowIf("show")] GameObject licorne;

    [Header("UI relatif")]
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_scoring;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_errors;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_rate;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_level;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_success;
    [SerializeField, ShowIf("ShowUI")] TMP_Text txt_laRecette;
    [SerializeField, ShowIf("ShowUI")] Image img_alimentTake;

    [Header("Refs � d'autres scripts")]
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
            laRecette = "Boulettes de Licorne fa�on p�ch� originel";
            listOfAlim.Add(new AlimentStruct("T�te de Licorne", licorne));
            listOfAlim.Add(new AlimentStruct("Pomme d'Eden", pomme));
            listOfAlim.Add(new AlimentStruct("Serpent en spaghetti", serpent));
        }
        if (recette == 2)
        {
            laRecette = "Steak de Scarab�e et ses d�lices magiques";
            listOfAlim.Add(new AlimentStruct("Scarab�e", scarab�e));
            listOfAlim.Add(new AlimentStruct("Haricots magiques", haricots));
            listOfAlim.Add(new AlimentStruct("Jus d'oeil de Gobelin", gobelin));
        }
        if (recette == 3)
        {
            laRecette = "Ramen Bajiru-Tatsu";
            listOfAlim.Add(new AlimentStruct("Cuisse de Dragon", dragon));
            listOfAlim.Add(new AlimentStruct("Champignon", champignon));
            listOfAlim.Add(new AlimentStruct("Sang de Basilic", basilic));
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
            noteSpawn.ThisRecette = listOfAlim; 
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
        //Debug.Log("L'aliment � prendre est :" + Scoreboard.alimentToTake);
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
            Scoreboard.successToAchieve = nbOfSucc1;
            alimentCol.GetComponent<AlimentCollision>().velocity = velocityLevel.x;
        }
        if (Scoreboard.level == 2)
        {
            Scoreboard.successToAchieve = nbOfSucc2;
            alimentCol.GetComponent<AlimentCollision>().velocity = velocityLevel.y;
        }
        if (Scoreboard.level == 3)
        {
            Scoreboard.successToAchieve = nbOfSucc3;
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
