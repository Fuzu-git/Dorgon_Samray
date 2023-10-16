using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Serialization;

public class BonusButton : MonoBehaviour
{
    [SerializeField] private NoteSpawn noteSpawn;
    [SerializeField] private AlimentCollision alimentCol;
    [SerializeField] private CuissonLevel cuissonLvl;
    private int _randomBonus;

    [SerializeField] private int cutterNoteBonus;
    [SerializeField] private int alimentNumberNoteBonus;
    [SerializeField] public float remainingTime; // Pour le mini-jeu 3
    public int alimentBonus;
    public int cutterNumberNoteBonus;

    public void ActivateBonus()
    {
        _randomBonus = Random.Range(0, 3);

        switch (_randomBonus)
        {
            case 0:
                alimentCol.bonus = true;
                alimentBonus = alimentNumberNoteBonus; // remplacé chef :')
                Debug.Log("Bonus 1 - aliment");
                break;

            case 1:
                noteSpawn.bonusIsActive = true;
                cutterNoteBonus = cutterNumberNoteBonus;
                Debug.Log("Bonus 2 - rythme");
                break;

            case 2:
                cuissonLvl.bonus3 = true;            //remplacé aussi
                cuissonLvl.remainingTimeBonus = remainingTime;
                Debug.Log("Bonus 3 - jauge");
                break;
        }
    }
}