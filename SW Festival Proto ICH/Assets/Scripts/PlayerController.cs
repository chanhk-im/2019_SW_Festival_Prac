using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Move();
        }
    }

    private void Move()
    {
        float _movingX = Input.GetAxisRaw("Horizontal");
        float _movingY = Input.GetAxisRaw("Vertical");

        Vector2 _velocityVector = new Vector2(_movingX * velocity, _movingY * velocity);

        rb.velocity = _velocityVector;
    }



}
