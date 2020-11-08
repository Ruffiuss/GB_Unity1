using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class LemonController : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject _trap;
        [SerializeField] private Transform _trapPosition;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce = 3.0f;
        [SerializeField] private float _trapForce = 1.0f;
        [SerializeField] private int _trapCount = 5;

        private Vector3 _moveDirection = Vector3.zero;
        private Rigidbody _playerRigidbody;

        private bool _isJumpCooldown = false;

        #endregion


        #region UnityMethods

        private void Start()
        {        
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
            {
                Move();
            }

            //if (Input.GetButton("Horizontal"))
            //{
            //    Rotate();
            //}

            if (Input.GetButtonDown("Jump"))
            {
                if (!_isJumpCooldown)
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
                _isJumpCooldown = false;
            }
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

        private void Move()
        {
            _moveDirection.z = Input.GetAxis("Vertical");
            _moveDirection.x = Input.GetAxis("Horizontal");
            _moveDirection.Normalize();

            var moveSpeed = _moveDirection * _speed;
            _playerRigidbody.velocity = moveSpeed;
        }

        private void Rotate()
        {
            _moveDirection.y = Input.GetAxis("Horizontal");
            transform.Rotate(0, _moveDirection.y * _rotationSpeed, 0);
        }

        private void Jump(float force)
        {
            var impulse = transform.up * _playerRigidbody.mass * force;
            _playerRigidbody.AddForce(impulse, ForceMode.Impulse);
            _isJumpCooldown = true;
        }

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
