using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Poele : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    [SerializeField] float speed;
    [SerializeField] GameObject firstLimit;
    [SerializeField] GameObject secondLimit;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePoele();
    }

    void MoveThePoele()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        if(transform.position.x >= firstLimit.transform.position.x && transform.position.x <= secondLimit.transform.position.x)
        transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;
        if (transform.position.x < firstLimit.transform.position.x)
            transform.position = new Vector3(secondLimit.transform.position.x, transform.position.y, transform.position.z);
        if (transform.position.x > secondLimit.transform.position.x)
            transform.position = new Vector3(firstLimit.transform.position.x, transform.position.y, transform.position.z);
    }
}
