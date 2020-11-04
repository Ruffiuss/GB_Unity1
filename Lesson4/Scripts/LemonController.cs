using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LemonController : MonoBehaviour
{

    #region Fields

    [SerializeField] private GameObject trap;
    [SerializeField] private Transform trapPosition;
    [SerializeField] private UnityEvent _onSpawned;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce = 3.0f;
    [SerializeField] private float _runForce = 1.0f;
    [SerializeField] private float _trapForce = 1.0f;
    [SerializeField] private int trapCount = 5;

    private Vector3 _moveDirection = Vector3.zero;
    private Rigidbody playerRigidbody;

    private bool isJumpCooldown = false;

    #endregion


    #region UnityMethods

    private void Start()
    {
        if (_onSpawned == null) _onSpawned = new UnityEvent();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            Move(_runForce);
        }

        if (Input.GetButton("Horizontal"))
        {
            Rotate();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumpCooldown)
            {
                Jump(_jumpForce);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            SetTrap(_trapForce);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumpCooldown = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TrapPack")
        {
            trapCount += 5;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Application.Quit();
        }
    }

    #endregion


    #region Methods

    private void Move(float force)
    {
        _moveDirection.z = Input.GetAxis("Vertical");

        var impulse = transform.forward * playerRigidbody.mass * force * _speed;
        playerRigidbody.AddForce(impulse, ForceMode.Force);
    }

    private void Rotate()
    {
        _moveDirection.y = Input.GetAxis("Horizontal");
        transform.Rotate(0, _moveDirection.y * _rotationSpeed, 0);
    }


    private void Jump(float force)
    {
        var impulse = transform.up * playerRigidbody.mass * force;
        playerRigidbody.AddForce(impulse, ForceMode.Impulse);
        isJumpCooldown = true;
    }     


    private void SetTrap(float force)
    {
        if (trapCount > 0)
        {

        Instantiate(trap, trapPosition.position, trapPosition.rotation);

        var impulse = transform.up * playerRigidbody.mass * force;
        var trapRigidBody = trap.GetComponent<Rigidbody>();
        trapRigidBody.AddForce(impulse, ForceMode.Impulse);

        trapCount--;
        }
    }

    #endregion
}
