using UnityEngine;
using UnityEngine.Events;

namespace HomeworksUnityLevel1
{


    public class LemonController : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject _trap;
        [SerializeField] private GameObject _bait;
        [SerializeField] private Transform _trapPosition;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _trapForce = 1.0f;
        [SerializeField] private int _trapCount = 5;
        [SerializeField] private int _baitCount = 3;
        [SerializeField] private float _health = 5.0f;

        private Rigidbody _playerRigidbody;
        private Animator _animator;
        private Vector3 _moveDirection = Vector3.zero;

        private float _currentSpeed;
        private float _animSpeed;
        private bool isEnemyContact = false;

        public bool IsEnemyContact => isEnemyContact;
        public int TrapCount => _trapCount;
        public int BaitCount => _baitCount;

        public float Health 
        {
            private get
            {
                return _health;
            }
            set
            {
                if (_health > 0)
                {
                    _health -= value;   
                }
                else
                {
                    Death();
                }
            }
        }

        #endregion


        #region UnityMethods

        private void Start()
        {        
            _playerRigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {

            _animSpeed = _playerRigidbody.velocity.magnitude;
            _animator.SetFloat("Speed", _animSpeed);

            _moveDirection.z = Input.GetAxis("Vertical");
            _moveDirection.x = Input.GetAxis("Horizontal");

            _moveDirection.Normalize();

            Vector3 lookForward = Vector3.RotateTowards(transform.forward, _moveDirection, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(lookForward);


            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = _runSpeed;
            }
            else
            {
                _currentSpeed = _walkSpeed;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                SetTrap(_trapForce);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                SetBait(_trapForce);
            }
        }

        private void FixedUpdate()
        {
            var speed = (_moveDirection.sqrMagnitude > 0) ? _currentSpeed : 0;
            speed = speed * Time.fixedDeltaTime;

            var moveDirection = transform.forward * speed;
            moveDirection.y = _playerRigidbody.velocity.y;

            _playerRigidbody.velocity = moveDirection;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "TrapPack")
            {
                _trapCount += 5;
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                isEnemyContact = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                isEnemyContact = false;
            }
        }

        #endregion


        #region Methods

        private void SetTrap(float force)
        {
            if (_trapCount > 0)
            {
                Instantiate(_trap, _trapPosition.position, _trapPosition.rotation);

                var impulse = transform.up * _playerRigidbody.mass * force;
                var trapRigidBody = _trap.GetComponent<Rigidbody>();
                trapRigidBody.AddForce(impulse, ForceMode.Impulse);

                _trapCount--;
            }
        }

        private void SetBait(float force)
        {
            if (_baitCount > 0)
            {
                Instantiate(_bait, _trapPosition.position, _trapPosition.rotation);

                var baitRigidBody = _bait.GetComponent<Rigidbody>();
                var impulse = transform.up * baitRigidBody.mass * force;
                baitRigidBody.AddForce(impulse, ForceMode.Impulse);

                _baitCount--;
            }
        }

        public float CheckHealth()
        {
            return _health;
        }

        private void Death()
        {
            Application.Quit();
        }

        #endregion


    }


}
