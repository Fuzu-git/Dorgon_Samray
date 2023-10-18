using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool show;
    private bool finished;
    private float timeRemaining;
    [SerializeField] float timerTotalTime;
    [SerializeField] TMP_Text timer;
    [SerializeField] TMP_Text txt_numberOfRecipe;


    [Header("Refereces de scripts")]

    [SerializeField, ShowIf("show")] NoteSpawn noteSpawn;
    [SerializeField, ShowIf("show")] Spawner spawner;
    [SerializeField, ShowIf("show")] CuissonLevel cuissonLevel;
    [SerializeField, ShowIf("show")] PowManager powManager;
    [SerializeField, ShowIf("show")] Poele lapoele;
    [SerializeField, ShowIf("show")] GameObject gordon;
    [SerializeField, ShowIf("show")] GameObject buttonBonus;

    private void FixedUpdate()
    {
        if (!finished)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining = timerTotalTime - Time.time;

            }
        }

        if(timeRemaining <= 60 && timeRemaining > 30)
        {
            //FirstChangeTempo();
        }
        if (timeRemaining <= 30 && timeRemaining > 0)
        {
           // SecondChangeTempo();
        }

        int timeLeft = ((int)timeRemaining);
        timer.text = timeLeft.ToString();

        if(timeRemaining <= 0)
        {
            finished = true;
            Endgame();
        }
    }

    private void FirstChangeTempo()
    {
        throw new NotImplementedException();
    }

    private void SecondChangeTempo()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        if (Scoreboard.nbOfRecipe == 3)
        { 
            Endgame ();
            Scoreboard.nbOfRecipe = 4;
        }

        txt_numberOfRecipe.text = Scoreboard.nbOfRecipe.ToString();
    }

    [Button]
    private void Endgame()
    {
        finished = true;
        Scoreboard.totalScore += (int)timeRemaining * 2;


        spawner.enabled = false;
        noteSpawn.enabled = false;
        cuissonLevel.enabled = false;
        powManager.enabled = false;
        lapoele.enabled = false;
        gordon.SetActive(true);
        buttonBonus.SetActive(true);
    }
}
