using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    public KeyCode noteColor;
    
    public bool isBonusNote = false; 

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.velocity = new Vector2(-speed, 0);
    }
}
