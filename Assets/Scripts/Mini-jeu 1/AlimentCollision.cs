using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlimentCollision : MonoBehaviour
{   
    [SerializeField] AlimentStruct Me;
    [SerializeField] AlimentManager alimentManager;
    [SerializeField] Spawner spawner;
    [SerializeField] string alimentType;
    [SerializeField] public float velocity;

    [Header("Bonus")]
    [SerializeField] public bool bonus;
    [SerializeField] private BonusButton _bonusButton;

    private void Awake()
    {
        if (alimentManager.listOfAlim != null)
        {
            if (!bonus)
            {
                if (spawner.countBetweenTwoCorrectAliment == spawner.CountBetweenTwoCorrectAlimentMax)
                {
                    Me = new AlimentStruct(Scoreboard.alimentToTake, Scoreboard.goodSprite);
                    spawner.countBetweenTwoCorrectAliment = 0;
                    //Debug.Log("Système anti-malchance activé, countbetween est égal à " + spawner);
                }
                else
                {
                    Me = alimentManager.listOfAlim[Random.Range(0, alimentManager.listOfAlim.Count)];

                }
            }
            else
            {
                Me = new AlimentStruct(Scoreboard.alimentToTake, Scoreboard.goodSprite);
                _bonusButton.alimentBonus--;

            }
            
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Me.Sprite;
            this.alimentType = Me.Name;

            if (Me.Name != Scoreboard.alimentToTake)
            { spawner.countBetweenTwoCorrectAliment++; }
            else
                spawner.countBetweenTwoCorrectAliment = 0;

        }
    }

    private void Update()
    {
        transform.position += new Vector3(0, -1, 0) * velocity * Time.deltaTime;

        if (_bonusButton.alimentBonus <= 0)
        {
            bonus = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(Me.Name == Scoreboard.alimentToTake)
            {
               //Debug.Log("Bien vu chacal");
                Scoreboard.sucessCounter++;
                Destroy(gameObject);
            }
            else
            {
               //Debug.Log("Noublard");
                Scoreboard.errorCounter++;
                Destroy(gameObject);
            }
        }
        if (col.gameObject.CompareTag("Despawner"))
        {
            if(Me.Name == Scoreboard.alimentToTake)
            {
               //Debug.Log("T'auras jamais la recette !");
                Scoreboard.rates++;
                Scoreboard.errorCounter++;
            }
        Destroy(gameObject);
        }
    }
}
