using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Serialization;

public class BonusButton : MonoBehaviour
{
    [SerializeField] private NoteSpawn noteSpawn;
    private int _randomBonus;

    [SerializeField] private int cutterNoteBonus;
    public int cutterNumberNoteBonus;

    public void ActivateBonus()
    {
        _randomBonus = Random.Range(0, 3);

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
    }
}