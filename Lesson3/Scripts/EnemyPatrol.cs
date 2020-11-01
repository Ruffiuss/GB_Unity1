using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyPatrol : MonoBehaviour
{
    #region Fields 

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float _speed;

    private Vector3 _moveDirection;
    private int currentWaypointIndex;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _moveDirection = Vector3.forward;
        currentWaypointIndex = 0;

    }

    void Update()
    {

        Patroling();  
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Application.Quit();
        }
    }

    #endregion


    #region Methods

    private void Patroling()
    {

        transform.LookAt(waypoints[currentWaypointIndex].transform);
        transform.position = Vector3.MoveTowards(transform.position,
                                                    waypoints[currentWaypointIndex].transform.position,
                                                    _speed * Time.deltaTime
                                                    );
        if (transform.position == waypoints[currentWaypointIndex].transform.position)
        {
            if (currentWaypointIndex == waypoints.Length - 1)
                currentWaypointIndex = 0;
            else
                currentWaypointIndex++;
        }
        
    }

    #endregion

}
