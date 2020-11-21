using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class Finish : MonoBehaviour
    {


        #region Fields

        [SerializeField] GameObject _finishDoor;

        private Transform _target;
        private Camera _camera;
        private byte leverProgress;
        private byte goal = 10;
        private bool isOpened = false;
        private bool isEnded = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            leverProgress = 0;
        }

        private void Update()
        {
            Debug.Log(leverProgress);
            if (leverProgress >= goal)
            {
                if(!isOpened)
                {
                    var animator = _finishDoor.GetComponentInChildren<Animator>();
                    animator.SetTrigger("GatesOpen");
                    isOpened = true;
                }
            }
            if (isEnded)
            {
                _camera.transform.LookAt(_target.transform.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameFinish();

                var rb = other.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * 100.0f, ForceMode.Impulse);

                _target = other.transform;
                _camera = GameObject.FindObjectOfType<Camera>();
            }
        }

        #endregion


        #region Methods

        private void GameFinish()
        {
            Debug.Log($"GAME END");
            isEnded = true;
        }

        public void setProgress(byte a)
        {
            leverProgress += a;
        }

        #endregion


    }


}