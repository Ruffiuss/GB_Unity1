using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class CameraSettings : MonoBehaviour
    {


        #region Fields

        [SerializeField] private Transform _target;
        [SerializeField] private float _lerpSpeed = 0.6f;
        [SerializeField] private float _cameraOffset = -2.0f;

        private Vector3 _targetPosition;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _targetPosition.y = transform.position.y;
        }

        private void Update()
        {
            _targetPosition.x = Mathf.Lerp(transform.position.x, _target.position.x, _lerpSpeed);
            _targetPosition.z = Mathf.Lerp(transform.position.z, _target.position.z, _lerpSpeed) + _cameraOffset;

            transform.position = _targetPosition;
        }


        #endregion


    }


}
