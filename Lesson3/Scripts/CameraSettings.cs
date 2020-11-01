using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    #region Fields

    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _my3DVector;
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private GameObject mainCamera;

    private Vector3 _moveDirection;
    private Vector3 cameraDistance;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _startRotation = _target.rotation;
        _moveDirection = _target.position;
        cameraDistance = _target.position;
    }

    private void Update()
    {
        //mainCamera.transform.LookAt(_target);
        //var angle = Quaternion.Angle(_target.rotation, transform.rotation );
        //if (angle != 0)
        //{
        //    RotateCameraFollowTarget(angle);
        //}
        //transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);

    }

    #endregion


    #region Methods

    private void RotateCameraFollowTarget(float angle)
    {
        transform.RotateAround(_target.position, Vector3.up, angle * Time.deltaTime * _rotationSpeed);
    }

    #endregion

}
