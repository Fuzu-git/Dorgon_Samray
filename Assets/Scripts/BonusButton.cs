using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BonusButton : MonoBehaviour
{
    [SerializeField] private NoteSpawn noteSpawn;
    private int _randomBonus;

    [SerializeField] public int cutterNoteBonus;
    [SerializeField] private int cutterNumberNoteBonus;

    private bool _isClicked = false;
    [SerializeField] private int bonusCooldown;
    [SerializeField] private Button thisButton; 

    public void ActivateBonus()
    {
        _randomBonus = Random.Range(0, 3);
        if (!_isClicked)
        {
            switch (_randomBonus)
            {
                case 0:
                    noteSpawn.bonusIsActive = true;
                    cutterNoteBonus = cutterNumberNoteBonus; //replace with MiniGame1 Bonus
                    break;

                case 1:
                    noteSpawn.bonusIsActive = true;
                    cutterNoteBonus = cutterNumberNoteBonus;
                    break;

                case 2:
                    noteSpawn.bonusIsActive = true;
                    cutterNoteBonus = cutterNumberNoteBonus; //replace with MiniGame3 Bonus
                    break;
            }

            StartCoroutine(CooldownBonus()); 
            _isClicked = true;
        }
    }

    private IEnumerator CooldownBonus()
    {
        thisButton.interactable = false; 
        yield return new WaitForSeconds(bonusCooldown);
            _isClicked = false;
            thisButton.interactable = true; 
    }
}