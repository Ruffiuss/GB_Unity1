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
        private AudioSourceChanger _audioChanger;
        private AudioSource _playerAudio;
        private Vector3 _moveDirection = Vector3.zero;

        private float _currentSpeed;
        private float _animSpeed;
        private bool isEnemyContact = false;
        private bool isGrounded = true;

        #endregion


        #region Properties

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
                    _audioChanger.SetAudioClip(1);
                    _playerAudio.Play();
                    _playerAudio.loop = false;
                    Debug.Log("Sound damage played");
                }
                else
                {
                    _audioChanger.ChangePitch(0.5f);
                    _audioChanger.SetAudioClip(1);
                    _playerAudio.Play();
                    _playerAudio.loop = false;
                }
            }
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            Time.timeScale = 1;
            _audioChanger = GetComponent<AudioSourceChanger>();
            _playerRigidbody = GetComponent<Rigidbody>();
            _playerAudio = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {

            _animSpeed = _playerRigidbody.velocity.magnitude;
            _animator.SetFloat("Speed", _animSpeed);

            if (isGrounded)
            {
                if (_animSpeed > 0.1)
                {
                    if (!_playerAudio.isPlaying)
                    {
                        _audioChanger.SetAudioClip(0);
                        _playerAudio.Play();
                    }
                }
                else
                {
                    if (_playerAudio.isPlaying)
                    {
                        if (_playerAudio.clip == _audioChanger.GetAudioClip(0))
                        {
                            _playerAudio.Stop();
                        }
                    }
                }
            }

            _moveDirection.z = Input.GetAxis("Vertical");
            _moveDirection.x = Input.GetAxis("Horizontal");

            _moveDirection.Normalize();

            Vector3 lookForward = Vector3.RotateTowards(transform.forward, _moveDirection, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(lookForward);

            if (Time.timeScale > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _audioChanger.ChangePitch(1.3f);
                    _currentSpeed = _runSpeed;
                }
                else
                {
                    _audioChanger.ChangePitch(1.0f);
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

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                isGrounded = true;
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

        private void Jump()
        {
            if (isGrounded)
            {
                _playerRigidbody.velocity += new Vector3(0,4.0f,0);
                isGrounded = false;
                _playerAudio.Stop();
            }
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

        #endregion


    }


}
