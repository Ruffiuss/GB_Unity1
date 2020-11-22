using UnityEngine;
using UnityEngine.UI;


namespace HomeworksUnityLevel1
{


    public class Finish : MonoBehaviour
    {


        #region Fields

        [SerializeField] GameObject _finishDoor;
        [SerializeField] Canvas _levelCompleteMenu;
        [SerializeField] LemonController _player;

        private Transform _target;
        private Camera _camera;
        private Text _endGameText;
        private float _playerHealth;
        private byte leverProgress;
        private byte goal = 10;
        private bool isOpened = false;
        private bool isEnded = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            leverProgress = 0;
            _endGameText = _levelCompleteMenu.GetComponentInChildren<Text>();

        }

        private void Update()
        {
            if (Time.timeScale > 0)
            {
                _playerHealth = _player.CheckHealth();
                if (_playerHealth <= 0)
                {
                    Time.timeScale = 0;
                    GameFinish("You dead", false);
                }
            }

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
                GameFinish("Level complete!", true);

                var rb = other.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * 100.0f, ForceMode.Impulse);

                _target = other.transform;
                _camera = GameObject.FindObjectOfType<Camera>();
            }
        }

        #endregion


        #region Methods

        public void GameFinish(string text, bool isGood)
        {
            if (isGood)
            {
                var buttons = _levelCompleteMenu.GetComponentsInChildren<Button>();
                buttons[0].enabled = false;
                buttons[0].gameObject.SetActive(false);
                buttons[1].enabled = true;
                _endGameText.text = text;
                isEnded = true;
                _levelCompleteMenu.enabled = true;
            }
            else
            {
                var buttons = _levelCompleteMenu.GetComponentsInChildren<Button>();
                buttons[0].enabled = true ;
                buttons[1].enabled = false;
                buttons[1].gameObject.SetActive(false);

                _endGameText.text = text;
                _levelCompleteMenu.enabled = true;
            }
        }

        public void setProgress(byte a)
        {
            leverProgress += a;
        }

        #endregion


    }


}