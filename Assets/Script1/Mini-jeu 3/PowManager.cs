using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PowManager : MonoBehaviour
{

    PlayerInput inputPow;
    [SerializeField]InputAction powAction;
    [Header("Relatif à la puissance")]
    // LE STATIC 
    public static int actualPower = 0;
    [SerializeField] Vector3 changePow;
    [Header("temps entre deux press")]
    [Header("GameObjects")]
    [SerializeField] GameObject flèche;
    [SerializeField] GameObject levier;


    void Start()
    {
        inputPow = GetComponent<PlayerInput>();
        powAction = inputPow.actions.FindAction("ChangePow");
    }

    private void Update()
    {
        ChangePower();
    }

    void ChangePower()
    {
         changePow = powAction.ReadValue<Vector3>();
            
            if (changePow.x > 0)
            {                
                actualPower = 1;
                Debug.Log(" actual power est " + actualPower);
                flèche.transform.position = new Vector3(120,475,0);
            }
            if (changePow.y > 0)
            {
                actualPower = 2;
                Debug.Log(" actual power est " + actualPower);
                flèche.transform.position = new Vector3(300, 475, 0);
            }
            if (changePow.z > 0)
            {
                actualPower = 3;
                Debug.Log(" actual power est " + actualPower);
                flèche.transform.position = new Vector3(480, 475, 0);
            }
        
        
    }
}
