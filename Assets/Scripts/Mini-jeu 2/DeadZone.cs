using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [HideInInspector] public int numberNoteMissed = 0;  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Note"))
        {
            var note = col.gameObject;
            numberNoteMissed++; 
            Debug.Log(numberNoteMissed);
            Destroy(note); 
        }
    }
}
