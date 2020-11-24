using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class EnemyPatrol : MonoBehaviour
    {


        #region Fields 

        [SerializeField] private GameObject[] _waypoints;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private float _speed;

        private GameObject _followingTarget;
        private Vector3 _currentWaypointPosition;

        private int _currentWaypointIndex;
        private bool isPatroiling = true;
        private bool isFollowing = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _currentWaypointIndex = 0;
            _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
        }

        private void Update()
        {
            if (isPatroiling)
            {
                Patroling();
            }
            if (isFollowing)
            {
                FollowThePlayer(_followingTarget);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {        
            if (collider.gameObject.CompareTag("Player"))
            {
                isPatroiling = false;
                isFollowing = true;
                _followingTarget = collider.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPatroiling = true;
                isFollowing = false;
                _followingTarget = null;
            }
        }

        #endregion


        #region Methods

        private void Patroling()
        {
            transform.LookAt(_currentWaypointPosition);
            transform.position = Vector3.MoveTowards(
                current: transform.position,
                target: _currentWaypointPosition,
                maxDistanceDelta: _speed * Time.deltaTime);

            if (transform.position == _currentWaypointPosition)
            {
                if (_currentWaypointIndex == _waypoints.Length - 1)
                {
                    _currentWaypointIndex = 0;
                    _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
                }
                else
                {
                    _currentWaypointIndex++;
                    _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
                }
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


}
