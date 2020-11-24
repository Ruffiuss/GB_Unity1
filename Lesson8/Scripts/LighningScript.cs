using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class LighningScript : MonoBehaviour
    {


        #region Fields


        private Light _lightSource;
        private Animator _lightAnimator;
        private AudioSourceChanger _lightningAudio;

        private float _lightningMaxCooldown = 10.0f;
        private float _lightningCooldown;
        private float _currentTime;
        private int _lightningExample = 0;
        private int _lightningAmount = 3;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _lightSource = GetComponent<Light>();
            _lightAnimator = GetComponent<Animator>();
            _lightningAudio = GetComponent<AudioSourceChanger>();
            _lightningMaxCooldown = SetLightningMaxCooldown();
            _lightningCooldown = SetLightningCooldown();
            _lightningExample = Random.Range(_lightningExample, _lightningAmount);
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                _lightAnimator.SetInteger("LightningExample", _lightningExample);
                _lightAnimator.SetTrigger("Lightning");
                _currentTime = _lightningCooldown;

                _lightningMaxCooldown = SetLightningMaxCooldown();
                _lightningExample = Random.Range(0, _lightningAmount);
                _lightningCooldown = SetLightningCooldown();

                _lightningAudio.PlayHitSound(Random.Range(0, _lightningAudio.Sounds));
            }
        }

        #endregion


        #region Methods

        private float SetLightningMaxCooldown()
        {
            return Random.Range(2.0f, 40.0f);
        }

        private float SetLightningCooldown()
        {
            return Random.Range(1.0f, _lightningMaxCooldown);
        }

        #endregion


    }


}
