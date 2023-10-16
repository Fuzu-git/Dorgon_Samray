using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlimentCollision : MonoBehaviour
{   
    [SerializeField] AlimentStruct Me;
    [SerializeField] Spawner spawner;
    [SerializeField] string alimentType;
    [SerializeField] public float velocity;

    [Header("Bonus")]
    [SerializeField] public bool bonus;
    [SerializeField] private BonusButton _bonusButton;

    private void Awake()
    {
        if (AlimentManager.listOfAlim != null)
        {
            if (!bonus)
            {
                if (spawner.countBetweenTwoCorrectAliment == spawner.CountBetweenTwoCorrectAlimentMax)
                {
                    Me = new AlimentStruct(Scoreboard.alimentToTake, Scoreboard.goodSprite);
                    spawner.countBetweenTwoCorrectAliment = 0;
                    Debug.Log("Syst�me anti-malchance activ�, countbetween est �gal � " + spawner);
                }
                else
                {
                    Me = AlimentManager.listOfAlim[Random.Range(0, AlimentManager.listOfAlim.Count)];

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
                Debug.Log("Bien vu chacal");
                Scoreboard.totalScore++;
                Scoreboard.sucessCounter++;
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
                Scoreboard.errorCounter++;
            }
        Destroy(gameObject);
        }
    }
}
