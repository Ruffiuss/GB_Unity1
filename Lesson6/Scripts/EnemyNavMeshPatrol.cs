using UnityEngine;
using UnityEngine.AI;


namespace HomeworksUnityLevel1
{


    public class EnemyNavMeshPatrol : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject[] _waypoints;
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private LayerMask _mask;

        private Transform _followingPosition;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _currentTargetPosition;
        private Vector3 _currentWaypointPosition;
        private Vector3 _lastTargetPosition;

        private float maxError = 5f;
        private int _currentWaypointIndex;
        private bool isPlayerInArea = false;
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
                _followingPosition = collider.gameObject.transform;
                isPlayerInArea = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPlayerInArea = false;
                isPatroiling = true;
                isFollowing = false;
                _followingPosition = null;
            }
        }

        private void FixedUpdate()
        {
            if (isPlayerInArea)
            {
                RaycastHit hit;

                var color = Color.red;

                var currentPositon = transform.position;
                currentPositon.y += 0.5f;
                var targetPosition = _followingPosition.position;
                targetPosition.y += 0.5f;

                var directionToTarget = targetPosition - currentPositon;

                var rayCast = Physics.Raycast(currentPositon, directionToTarget, out hit, directionToTarget.magnitude, _mask);
                if (rayCast)
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        isPatroiling = false;
                        isFollowing = true;
                        _followingPosition = hit.transform;
                        Debug.Log($"{_followingPosition.position}");
                        color = Color.green;
                    }
                    else
                    {
                        isPatroiling = true;
                        isFollowing = false;
                        color = Color.red;
                    }
                }
                Debug.DrawRay(currentPositon, directionToTarget, color);
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

            var distanceToTarget = OptimizedCheckDistance(_navMeshAgent.transform.position, _currentWaypointPosition);

            if (distanceToTarget <= 0.05f)
            {
                if (_currentWaypointIndex == _waypoints.Length - 1)
                {
                    _currentWaypointIndex = 0;
                    _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
                    _navMeshAgent.ResetPath();
                    _navMeshAgent.SetDestination(_currentWaypointPosition);
                }
                else
                {
                    _currentWaypointIndex++;
                    _currentWaypointPosition = _waypoints[_currentWaypointIndex].transform.position;
                    _navMeshAgent.ResetPath();
                    _navMeshAgent.SetDestination(_currentWaypointPosition);
                }
            }
        }

        private void FollowThePlayer()
        {
            _currentTargetPosition = _followingPosition.position;

            if (OptimizedCheckDistance(_currentTargetPosition, _lastTargetPosition) > maxError)
                _lastTargetPosition = _currentTargetPosition;

            _navMeshAgent.SetDestination(_lastTargetPosition);
        }

        #endregion


    }


}
