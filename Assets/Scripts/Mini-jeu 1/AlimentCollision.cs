using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlimentCollision : MonoBehaviour
{   
    [SerializeField] AlimentStruct Me;
    [SerializeField] string alimentType;

    private void Awake()
    {
        if (AlimentManager.listOfAlim != null)
        {

            Me = AlimentManager.listOfAlim[Random.Range(0, AlimentManager.listOfAlim.Count)];
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Me.Sprite;
            this.alimentType = Me.Name;
        }
    }

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(Me.Name == Scoreboard.alimentToTake)
            {
                Debug.Log("Bien vu chacal");
                Scoreboard.totalScore++;
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Noublard");
                Scoreboard.errorCounter++;
                Destroy(gameObject);
            }
        }
        if (col.gameObject.CompareTag("Despawner"))
        {
            if(Me.Name == Scoreboard.alimentToTake)
            {
                Debug.Log("T'auras jamais la recette !");
                Scoreboard.rates++;
            }
        Destroy(gameObject);
        }
    }
}
