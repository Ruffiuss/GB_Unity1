using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class DoorOpener : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject _targetGates;
        [SerializeField] private GameObject _targetLamp;
        [SerializeField] private Finish _progress;

        private Animator _currentAnimator;
        private bool isPlayerInArea = false;
        private bool isLeverActivated = false;

        #endregion


        #region UnityMethods

        private void Update()
        {
            LeverActivate();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isPlayerInArea = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isPlayerInArea = false;
            }
        }

        #endregion


        #region Methods

        private void LeverActivate()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (isPlayerInArea)
                {
                    if (!isLeverActivated)
                    {
                        _currentAnimator = GetComponentInChildren<Animator>();
                        _currentAnimator.SetTrigger("LeverActivate");
                        isLeverActivated = true;

                        _currentAnimator = _targetGates.GetComponentInChildren<Animator>();
                        _currentAnimator.SetTrigger("GatesOpen");

                        _currentAnimator = _targetLamp.GetComponent<Animator>();
                        _currentAnimator.SetTrigger("Activate");

                        _progress.setProgress((byte)1); 
                    }
                }
            }
        }

        #endregion


    }


}

