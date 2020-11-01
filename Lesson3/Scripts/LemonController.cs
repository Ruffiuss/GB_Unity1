using System;
using UnityEngine;


public class LemonController : MonoBehaviour
{

    #region Fields

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject trap;
    [SerializeField] private Transform trapPosition;
    [SerializeField] private Quaternion _startPosition;

    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveRotation = Vector3.zero;

    #endregion


    #region UnityMethods

    private void Update()
    {

        _startPosition = transform.rotation;

        Move();
        Rotate();
        Jump();
        SetTrap();

    }

    #endregion

    #region Methods

    private void Move()
    {
        _moveDirection.z = Input.GetAxis("Vertical");
        _moveDirection.y = Input.GetAxis("Horizontal");
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (Input.GetButton("Horizontal"))
        {
            _moveDirection.y = Input.GetAxis("Horizontal");
            transform.Rotate(0, _moveDirection.y * _rotationSpeed, 0);
        }
    }


    private void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            transform.position += transform.up;
        }

    }     


    private void SetTrap()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(trap, trapPosition.position, trapPosition.rotation);
        }
    }

    #endregion
}
