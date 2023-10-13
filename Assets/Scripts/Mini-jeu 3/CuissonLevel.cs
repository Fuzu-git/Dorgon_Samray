using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
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
    [SerializeField, ShowIf("Show")] GameObject gordon;
    [SerializeField, ShowIf("Show")] GameObject service;
    [SerializeField] float reduceJaugeTime = 3f;
    public bool DebugOnly;
    [SerializeField, ShowIf("DebugOnly")] private float reduceHeatTimer;


    // Start is called before the first frame update
    void Start()
    {
        interpolater = 0;
        palierOne = 1f / 3f;
        palierTwo = palierOne * 2;
        palierThree = palierOne * 3;
        reduceHeatTimer = reduceJaugeTime;
    }
    private void FixedUpdate()
    {
        if (!finished)
        {
            if (Scoreboard.totalTime >= 0)
            {
                Scoreboard.totalTime = timerTotalTime - Time.time;

            }
        }
        
        int timeLeft = ((int)Scoreboard.totalTime);
        timer.text = timeLeft.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, Mathf.Lerp(max, min, interpolater), 0);

        if(!finished)
        {
            IncrementHeat();
            Cooldown();

            if (PowManager.actualPower == 3)
            {
                service.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("Service !");
                    Endgame();
                }
            }
            else
                service.SetActive(false);

            if (Scoreboard.totalTime <= 0)
            {
                Endgame();
            }

            if (interpolater >= 1)
                Endgame();

        }
        else
        {
            Restart();
        }      

    }

    void IncrementHeat()
    {
        if (Input.GetKeyDown(KeyCode.Space) && interpolater <= 1)
        {
            
            Debug.Log("ça incrémente");

            // Je laisse les 3 if en prévision de si des différences s'opèrent dans le futur en fonction des paliers
            if (PowManager.actualPower == 1 && interpolater <= palierOne)
            {
                interpolater += (1 / totalNumberOfPush);
                reduceHeatTimer = reduceJaugeTime;
            }
            if (PowManager.actualPower == 2 && interpolater <= palierTwo && interpolater >= palierOne)
            {
                interpolater += (1 / totalNumberOfPush);
                reduceHeatTimer = reduceJaugeTime;
            }
            if (PowManager.actualPower == 3 && interpolater <= palierThree && interpolater >= palierTwo)
            {
                interpolater += (1 / totalNumberOfPush);
                reduceHeatTimer = reduceJaugeTime;
            }

            
        }
    }

    void Cooldown()
    {
        reduceHeatTimer -= Time.deltaTime;
        if (reduceHeatTimer <= 0) 
        { 
            if(interpolater > 0)
            {
                reduceHeatTimer = reduceJaugeTime;
                interpolater -= (1/ totalNumberOfPush);
            }
        }
    }

    void Endgame()
    {
        gordon.SetActive(true);
        finished = true;
    }

    private void Restart()
    {
        if (vit.action.ReadValue<Vector3>().x > 0)
        {
            Debug.Log("C'est reparti, au fourneau");
            finished = false;
            gordon.SetActive(false);
            interpolater = 0;
        }
    }
}
