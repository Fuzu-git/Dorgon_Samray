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
    InputAction powAction;
    [Header("Relatif � la puissance")]
    // LE STATIC 
    public static int actualPower = 0;
    [SerializeField] Vector2 changePow;
    [Header("temps entre deux press")]
    private float nextPressTime;
    [SerializeField] float secondsBetweenTwoPress = 0;
    [Header("GameObjects")]
    [SerializeField] GameObject fl�che;
    [SerializeField] GameObject levier;


    // Start is called before the first frame update
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
         changePow = powAction.ReadValue<Vector2>();
        if(Time.time > nextPressTime)
        {
            nextPressTime = Time.time + secondsBetweenTwoPress;
            
            if (changePow.y > 0 && actualPower < 3 )
            {                
                actualPower++;
                Debug.Log(" actual power est " + actualPower);
                fl�che.transform.localPosition += new Vector3(180,0,0);
                levier.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (changePow.y < 0 && actualPower > 1)
            {
                actualPower--;
                Debug.Log(" actual power est " + actualPower);
                fl�che.transform.localPosition += new Vector3(-180, 0, 0);
                levier.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
    }
}
