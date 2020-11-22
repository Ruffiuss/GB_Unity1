﻿using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class DoorOpener : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject _targetGates;
        [SerializeField] private GameObject _targetLamp;
        [SerializeField] private GameObject _tipsMenu;
        [SerializeField] private Finish _progress;

        private Animator _currentAnimator;
        private AudioSource _audioSource;
        private PlayerTips _tips;
        private ParticlesController _targetParticle;
        private bool isPlayerInArea = false;
        private bool isLeverActivated = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _tips = _tipsMenu.GetComponent<PlayerTips>();
            _audioSource = GetComponent<AudioSource>();
            _targetParticle = _targetGates.GetComponent<ParticlesController>();
        }

        private void Update()
        {
            LeverActivate();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isPlayerInArea = true;
                if (!isLeverActivated)
                {
                    _tips.Activate = true;
                }
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isPlayerInArea = false;
                _tips.Activate = false;
            }
        }

        #endregion


        #region Methods

        private void LeverActivate()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isPlayerInArea)
                {
                    if (!isLeverActivated)
                    {
                        _audioSource.Play();

                        _targetParticle.MakeParticles();

                        _currentAnimator = GetComponentInChildren<Animator>();
                        _currentAnimator.SetTrigger("LeverActivate");
                        isLeverActivated = true;

                        _currentAnimator = _targetGates.GetComponentInChildren<Animator>();
                        _currentAnimator.SetTrigger("GatesOpen");

                        _currentAnimator = _targetLamp.GetComponent<Animator>();
                        _currentAnimator.SetTrigger("Activate");

                        _progress.setProgress((byte)1);

                        _tips.Activate = false;
                    }
                }
            }
        }

        #endregion


    }


}

