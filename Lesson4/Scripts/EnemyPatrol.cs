using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyPatrol : MonoBehaviour
{
    #region Fields 

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float _speed;

    private GameObject foloowingTarget;
    private Vector3 _moveDirection;

    private int currentWaypointIndex;
    private bool isPatroiling = true;
    private bool isFollowing = false;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _moveDirection = Vector3.forward;
        currentWaypointIndex = 0;
    }

    void Update()
    {
        if (isPatroiling)
        {
            Patroling();
        }
        if (isFollowing)
        {
            FollowThePlayer(foloowingTarget);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {        
        if (collider.gameObject.CompareTag("Player"))
        {
            isPatroiling = false;
            isFollowing = true;
            foloowingTarget = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            isPatroiling = true;
            isFollowing = false;
            foloowingTarget = null;
        }
    }

    #endregion


    #region Methods

    private void Patroling()
    {
        transform.LookAt(waypoints[currentWaypointIndex].transform);
        transform.position = Vector3.MoveTowards(
            current: transform.position,
            target: waypoints[currentWaypointIndex].transform.position,
            maxDistanceDelta: _speed * Time.deltaTime);

        if (transform.position == waypoints[currentWaypointIndex].transform.position)
        {
            if (currentWaypointIndex == waypoints.Length - 1)
                currentWaypointIndex = 0;
            else
                currentWaypointIndex++;
        }        
    }

    private void FollowThePlayer(GameObject gameObject)
    {
        transform.LookAt(gameObject.transform);
        transform.position = Vector3.MoveTowards(
            current: transform.position,
            target: gameObject.transform.position,
            maxDistanceDelta: _speed * Time.deltaTime);
    }

    #endregion

}
