using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class LighningScript : MonoBehaviour
    {


        #region Fields

        private Light _lightSource;

        private float _lightningMaxCooldown = 10.0f;
        private float _lightningCooldown;
        private float _lightningLenght = 100.0f;
        private float _lightningScale;
        private float _currentTime;
        private float _currentLightningLenghtTime;
        private bool _isLighningCooldown;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _lightSource = GetComponent<Light>();
            _lightningMaxCooldown = Random.Range(5.0f, 10.0f);
            _lightningCooldown = Random.Range(0, _lightningMaxCooldown);
            _currentLightningLenghtTime = 0.01f;
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                _currentTime = _lightningCooldown;
                _isLighningCooldown = false;
                _lightSource.intensity = 0.1f;
            }
            else
            {
                _isLighningCooldown = true;
                _lightningMaxCooldown = Random.Range(0, 20.0f);
                _lightningCooldown = Random.Range(0, _lightningMaxCooldown);
            }

            if (!_isLighningCooldown)
            {
                if (_currentLightningLenghtTime <= _lightningLenght)
                {
                    _lightningScale = Random.Range(0.7f, 1.0f);
                    _lightSource.intensity = _lightningScale;
                    _currentLightningLenghtTime++;
                }
                else
                {
                    _currentLightningLenghtTime = 0.1f;
                }
            }
            else
            {
                _lightSource.intensity = 0.1f;
            }
        }

        #endregion


    }


}
