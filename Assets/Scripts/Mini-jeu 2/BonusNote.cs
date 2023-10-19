using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusNote : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.velocity = new Vector2(-speed, 0);
    }
}
