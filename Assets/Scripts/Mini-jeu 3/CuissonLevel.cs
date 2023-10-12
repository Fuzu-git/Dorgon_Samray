using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CuissonLevel : MonoBehaviour
{
    [SerializeField] float min;
    [SerializeField] float max;
    [SerializeField] float interpolater;
    [SerializeField] float totalNumberOfPush;
    public bool Show;
    [SerializeField,ShowIf("Show")] float palierOne;
    [SerializeField, ShowIf("Show")] float palierTwo;
    [SerializeField, ShowIf("Show")] float palierThree;


    //PlayerInput inputInc;
   // [SerializeField]InputAction incAction;

    // Start is called before the first frame update
    void Start()
    {
        interpolater = 0;
        palierOne = 1f / 3f;
        palierTwo = palierOne * 2;
        palierThree = palierOne * 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0,Mathf.Lerp(max,min,interpolater),0);
        if (Input.GetKeyDown(KeyCode.Space) && interpolater <= 1)
        {
            Debug.Log("ça incrémente");
            if(PowManager.actualPower == 1 && interpolater <= palierOne)
            {
            interpolater += (1 / totalNumberOfPush);
            }
            if (PowManager.actualPower == 2 && interpolater <= palierTwo)
            {
                interpolater += (1 / totalNumberOfPush);
            }
            if (PowManager.actualPower == 3 && interpolater <= palierThree)
            {
                interpolater += (1 / totalNumberOfPush);
            }
        }
        
    }
}
