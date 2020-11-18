using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class LemonController : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject _trap;
        [SerializeField] private Transform _trapPosition;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _trapForce = 1.0f;
        [SerializeField] private int _trapCount = 5;

        private Rigidbody _playerRigidbody;
        private Animator _animator;
        private Vector3 _moveDirection = Vector3.zero;

        private float _currentSpeed;
        private float _animSpeed;

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
                Application.Quit();
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

        #endregion


    }


}
