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
    [SerializeField] GameObject goPos1;
    [SerializeField] GameObject goPos2;
    [SerializeField] GameObject goPos3;


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
                flèche.transform.position = goPos1.transform.position;
        }
            if (changePow.y > 0)
            {
                actualPower = 2;
                Debug.Log(" actual power est " + actualPower);
                flèche.transform.position = goPos2.transform.position;
        }
            if (changePow.z > 0)
            {
                actualPower = 3;
                Debug.Log(" actual power est " + actualPower);
                flèche.transform.position = goPos3.transform.position;
        }
        
        
    }
}
