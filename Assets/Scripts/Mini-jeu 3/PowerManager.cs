using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class PowerManager : MonoBehaviour
{
    [Header("Points à gagner")]
    [SerializeField] int score = 0;
    [SerializeField] int scoreToWin = 10;
    [SerializeField] int tolerance = 0;

    [Header("Puissances minimales et maximales à atteindres")]
    [SerializeField] int minPower = 100;
    [SerializeField] int maxPower = 180;

    [Header("Puissances")]
    [SerializeField] int powerToReach;
    [SerializeField] int actualPower = 0;
    [SerializeField] int incrementalInt = 0;

    [SerializeField] bool ShowUI;

    [Header("UI")]
    [SerializeField, ShowIf("ShowUI")] TMP_Text ui_powerToReach;
    [SerializeField, ShowIf("ShowUI")] TMP_Text ui_actualPower;
    [SerializeField, ShowIf("ShowUI")] TMP_Text ui_score;
    [SerializeField, ShowIf("ShowUI")] TMP_Text ui_texteUnderScore;

    [SerializeField, ShowIf("ShowUI")] GameObject scoreBoard;
    [SerializeField, ShowIf("ShowUI")] TMP_Text ui_incrementalInt;          

    void Start()
    {
        scoreBoard.SetActive(false);
        actualPower = 0;
        incrementalInt = 0;
        powerToReach = Random.Range(minPower, maxPower);
        ui_powerToReach.text = powerToReach.ToString();
    }
    
    void Update()
    {
        ui_actualPower.text = actualPower.ToString();
        ui_incrementalInt.text = incrementalInt.ToString();

        if (actualPower == powerToReach)
        {
            Debug.Log("Bravo, tu as atteint la bonne cuisson");
        }
        if(actualPower > powerToReach)
        {
            Debug.Log("Aie aie aie, ça brûle");
        }
    }

    public void PowerOne()
    {
        incrementalInt = 1;
    }

    public void PowerTwo()
    {
        incrementalInt = 5;
    }

    public void PowerThree()
    {
        incrementalInt = 10;
    }

    public void IncrementPower()
    {
        actualPower += incrementalInt;
    }

    public void EndPlat()
    {
        if(actualPower == powerToReach)
        {
            score += scoreToWin;
            Debug.Log("Vous avez gagné " + scoreToWin + " points, bravo tu possèdes la recette");
            ui_texteUnderScore.text = "Tu possèdes la recette !!!";
        }
        else
        {
            if(actualPower > powerToReach)
                tolerance = scoreToWin - (actualPower - powerToReach);
            if(actualPower < powerToReach)
                tolerance = scoreToWin - (powerToReach - actualPower);

            if ( tolerance < 0)
            {
                tolerance *= -1;
            }
            if(tolerance > 0 &&  tolerance < scoreToWin)
            {
                scoreToWin -= tolerance;
                score += scoreToWin;
                ui_texteUnderScore.text = "Pas mal, tu es proche de la recette";
                Debug.Log("Vous avez gagné " + scoreToWin + " points, tu t'approches de la recette");
            }
            else
            {
                ui_texteUnderScore.text = "Vous n'avez pas la recette";
                Debug.Log("Tu n'as pas la recette ... looser");
            }
        }
        ui_score.text = score.ToString();
        scoreBoard.SetActive(true);
    }
}
