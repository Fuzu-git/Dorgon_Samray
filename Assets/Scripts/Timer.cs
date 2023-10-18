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
    private float timerTotalTime;
    [SerializeField] TMP_Text timer;

    [Header("Refereces de scripts")]

    [SerializeField, ShowIf("show")] NoteSpawn noteSpawn;
    [SerializeField, ShowIf("show")] Spawner spawner;
    [SerializeField, ShowIf("show")] CuissonLevel cuissonLevel;
    [SerializeField, ShowIf("show")] PowManager powManager;

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
            FirstChangeTempo();
        }
        if (timeRemaining <= 30 && timeRemaining > 0)
        {
            SecondChangeTempo();
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
        if (show/*Scoreboard.nbOfRecipe == 3*/)
        {
            finished = true;
            Scoreboard.totalScore += (int)timeRemaining*2;
            Endgame ();
        }

        
    }

    private void Endgame()
    {
        noteSpawn.enabled = false;
        spawner.enabled = false;
        cuissonLevel.enabled = false;
        powManager.enabled = false;
    }
}
