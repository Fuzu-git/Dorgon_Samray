using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class CuissonLevel : MonoBehaviour
{
    public bool Show;
    [SerializeField, ShowIf("Show")] float min;
    [SerializeField, ShowIf("Show")] float max;
    [SerializeField, ShowIf("Show")] float interpolater;
    [SerializeField] float timerTotalTime;
    [SerializeField] float totalNumberOfPush;
    [SerializeField, ShowIf("Show")] float palierOne;
    [SerializeField, ShowIf("Show")] float palierTwo;
    [SerializeField, ShowIf("Show")] float palierThree;
    [SerializeField, ShowIf("Show")] bool finished = false;
    [SerializeField, ShowIf("Show")] InputActionProperty vit;

    [Header("UI")]
    [SerializeField, ShowIf("Show")] TMP_Text timer;
    [SerializeField, ShowIf("Show")] GameObject service;
    [SerializeField] float reduceJaugeTime = 3f;
    public bool DebugOnly;
    [SerializeField, ShowIf("DebugOnly")] private float reduceHeatTimer;
    [Header("Bonus")]
    [SerializeField] public bool bonus3;
    [SerializeField] private BonusButton _bonusButton;
    public float remainingTimeBonus;
    [Header("References ")]
    [SerializeField] GameObject activation;
    [SerializeField] PowManager powManager;

    [HideInInspector] public float Interpolater { get => interpolater; }
    [HideInInspector] public float PalierOne { get => palierOne; }
    [HideInInspector] public float PalierTwo { get => palierTwo; }
    [HideInInspector] public float PalierThree { get => palierThree; }
    [HideInInspector] public float TotalNumberOfPush { get => totalNumberOfPush; }

    // Start is called before the first frame update
    void Start()
    {
        activation.SetActive(true);
        interpolater = 0;
        palierOne = (8 / totalNumberOfPush);
        palierTwo = (15 / totalNumberOfPush);
        palierThree = 1f;
        reduceHeatTimer = reduceJaugeTime;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (bonus3)
        {
            remainingTimeBonus -=  Time.deltaTime;
        }

        transform.localPosition = new Vector3(0, Mathf.Lerp(max, min, interpolater), 0);

        if(!finished)
        {
            IncrementHeat();
            Cooldown();

            if (PowManager.actualPower == 3)
            {
                service.SetActive(true);                
            }
            else
                service.SetActive(false);

            
            if (interpolater >= 1 && interpolater <2)
            {
                Scoreboard.totalScore += 15;
                finished = true;
                interpolater = 2;

                // LA FIN
                Scoreboard.nbOfRecipe++;
            }

        }
        else
        {
            Restart();
        }   
            
        if(remainingTimeBonus < 0)
        {
            bonus3 = false;
        }

    }

    void IncrementHeat()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && interpolater <= 1)
        {
         
            if(remainingTimeBonus > 0)
            {
                interpolater += (2 / totalNumberOfPush);
                reduceHeatTimer = reduceJaugeTime;
            }
            else
            {
                // Je laisse les 3 if en prévision de si des différences s'opèrent dans le futur en fonction des paliers
                if (PowManager.actualPower == 1 && interpolater <= palierOne)
                {
                    interpolater += (1 / totalNumberOfPush);
                    reduceHeatTimer = reduceJaugeTime;
                }
                if (PowManager.actualPower == 2 && interpolater <= palierTwo && interpolater >= palierOne - 2/totalNumberOfPush)
                {
                    interpolater += (1 / totalNumberOfPush);
                    reduceHeatTimer = reduceJaugeTime;
                }
                if (PowManager.actualPower == 3 && interpolater <= palierThree && interpolater >= palierTwo - 2 / totalNumberOfPush)
                {
                    interpolater += (1 / totalNumberOfPush);
                    reduceHeatTimer = reduceJaugeTime;
                }
                
            }
                       
        }
    }

    void Cooldown()
    {
        reduceHeatTimer -= Time.deltaTime;
        if (reduceHeatTimer <= 0) 
        {
            if (interpolater > 0 && PowManager.actualPower == 1)
            {
                reduceHeatTimer = reduceJaugeTime;
                interpolater -= (1 / totalNumberOfPush);
            }
            else if (interpolater > palierOne && PowManager.actualPower == 2)
            {
                reduceHeatTimer = reduceJaugeTime;
                interpolater -= (1/ totalNumberOfPush);
            }
            else if (interpolater > palierTwo && PowManager.actualPower == 3)
            {
                reduceHeatTimer = reduceJaugeTime;
                interpolater -= (1 / totalNumberOfPush);
            }
        }
    }

    private void Restart()
    {
        if (vit.action.ReadValue<Vector3>().x > 0)
        {
            Debug.Log("C'est reparti, au fourneau");
            finished = false;
            powManager.bonus1 = true;
            powManager.bonus2 = true;
            interpolater = 0;
        }
    }
}
