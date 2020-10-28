using System;
using UnityEngine;

public class LemonController : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    private Vector3 _moveDirection = Vector3.zero;

    private void Start()
    {
        //var rigidBody = gameObject.GetComponent<Rigidbody>();
        //rigidBody.mass = 2;

        //var collider = gameObject.GetComponent<Collider>();
        //collider.enabled = false;
    }

    private void Update()
    {
        Move();
        Jump(); 
    }

    private void Jump()
    {
        //Прыжок
        if (Input.GetButtonDown("Jump"))
        {
            transform.position += transform.up;

        }
    }

    private void Move()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.z = Input.GetAxis("Vertical");
        transform.position += _moveDirection * Time.deltaTime * _speed;
    }
}
