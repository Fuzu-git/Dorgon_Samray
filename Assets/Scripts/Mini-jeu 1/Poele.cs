using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Poele : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    [SerializeField] float speed;
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
        transform.position += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;
    }
}
