using UnityEngine;
using UnityEngine.AI;


namespace HomeworksUnityLevel1
{


    public class EnemyNavMeshPatrol : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject[] _waypoints;
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private Transform _followingPosition;

        private NavMeshAgent _navMeshAgent;
        private Vector3 _currentTargetPosition;
        private Vector3 _currentWaypointPosition;
        private Vector3 _lastTargetPosition;

        private float maxError = 5f;
        private int _currentWaypointIndex;
        private bool isPlayerInArea = true;
        private bool isPatroiling = true;
        private bool isFollowing = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _currentWaypointIndex = 0;
            _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;

            _currentTargetPosition = _playerPosition.position;
            _lastTargetPosition = _currentTargetPosition;

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.SetDestination(_currentWaypointPosition);
        }

        private void Update()
        {
            if (isPatroiling)
            {
                Patroling();
            }
            if (isFollowing)
            {
                FollowThePlayer();
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isPatroiling = false;
                isFollowing = true;
                _followingPosition = collider.gameObject.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPatroiling = true;
                isFollowing = false;
                _followingPosition = null;
            }
        }

        #endregion


        #region Methods

        private float OptimizedCheckDistance(Vector3 firstPosition, Vector3 secondPosition)
        {
            return (firstPosition - secondPosition).sqrMagnitude;
        }

        private void Patroling()
        {
            _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
            _navMeshAgent.SetDestination(_currentWaypointPosition);

            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (_currentWaypointIndex == _waypoints.Length - 1)
                {
                    _currentWaypointIndex = 0;
                    _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
                    _navMeshAgent.SetDestination(_currentWaypointPosition);
                }
                else
                {
                    _currentWaypointIndex++;
                    _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
                    _navMeshAgent.SetDestination(_currentWaypointPosition);
                }
            }
        }

        private void FollowThePlayer()
        {
            _currentTargetPosition = _playerPosition.position;

            if (OptimizedCheckDistance(_currentTargetPosition, _lastTargetPosition) > maxError)
                _lastTargetPosition = _currentTargetPosition;

            _navMeshAgent.SetDestination(_lastTargetPosition);
        }

        #endregion


    }


}
