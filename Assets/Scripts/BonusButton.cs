using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BonusButton : MonoBehaviour
{
    [SerializeField] private NoteSpawn noteSpawn;
    private int _randomBonus;
    
    public void ActivateBonus()
    {
        _randomBonus = Random.Range(0, 3);
        
        switch (_randomBonus)
        {
            case 0: 
                noteSpawn.bonusIsActive = true; //replace with MiniGame1 Bonus
            break; 
            
            case 1:
                noteSpawn.bonusIsActive = true;
            break; 
            
            case 2:
                noteSpawn.bonusIsActive = true;  //replace with MiniGame3 Bonus
            break;
        }
    }
}
