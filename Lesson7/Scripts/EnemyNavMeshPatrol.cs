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

        private GameObject _player;
        private Transform _followingPosition;
        private NavMeshAgent _navMeshAgent;
        private LemonController _lemonController;
        private Vector3 _currentTargetPosition;
        private Vector3 _currentWaypointPosition;
        private Vector3 _lastTargetPosition;

        private int _currentWaypointIndex;
        private float _currentTime;
        private float _attackCooldown = 2.0f;
        private float maxError = 5f;
        private bool isPlayerInArea = false;
        private bool isPatroiling = true;
        private bool isFollowing = false;
        private bool isBaited = false;
        private bool isContact = false;
        private bool attackCooldown = false;

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

                if (isContact)
                {
                    Attack(_player);
                }
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("FakePlayer"))
            {
                isBaited = true;
                _followingPosition = collider.gameObject.transform;
                _player = collider.gameObject;
                isContact = true;
                isPlayerInArea = true;
            }
            if (!isBaited)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    _followingPosition = collider.transform;
                    _player = collider.gameObject;
                    _lemonController = _player.GetComponent<LemonController>();
                    isContact = true;
                    isPlayerInArea = true;
                }
            }            
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isBaited)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    isPatroiling = true;
                    isFollowing = false;
                    isPlayerInArea = false;
                    _followingPosition = null;
                    _player = null;
                    _lemonController = null;
                }
            }
            if (other.gameObject.CompareTag("FakePlayer"))
            {
                isPatroiling = true;
                isFollowing = false;
                isPlayerInArea = false;
                _followingPosition = null;
                _player = null;
                _lemonController = null;
            }
        }

        private void FixedUpdate()
        {
            if (isPlayerInArea && (_followingPosition != null))
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
                        color = Color.green;
                    }
                    else if (hit.collider.gameObject.CompareTag("FakePlayer"))
                    {
                        isPatroiling = false;
                        isFollowing = true;
                        _followingPosition = hit.transform;
                        color = Color.yellow;
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
            if (_followingPosition != null)
            {
                _currentTargetPosition = _followingPosition.position;

                if (OptimizedCheckDistance(_currentTargetPosition, _lastTargetPosition) > maxError)
                    _lastTargetPosition = _currentTargetPosition;

                _navMeshAgent.SetDestination(_lastTargetPosition);
            }
            else
            {
                isPlayerInArea = false;
                isPatroiling = true;
                isFollowing = false;
                isBaited = false;
                isContact = false;
            }        
        }

        private void Attack(GameObject target)
        {
            if (_lemonController != null)
            {
                if (_lemonController.IsEnemyContact)
                {
                    _currentTime -= Time.deltaTime;

                    if (_currentTime <= 0)
                    {
                        _currentTime = _attackCooldown;
                        _lemonController.Health = 1;
                    }
                }
            }
        }

        #endregion


    }


}
