using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    #region Fields

    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private Quaternion _startRotation2;
    [SerializeField] private Vector3 _my3DVector;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _speed;

    private Vector3 startRayCastPosition; 
    private Vector3 targetRayCastPosition;
    private Vector3 distanceToPlayer;

    private float yCoordinate;
    private float cameraDistance;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _startRotation = _target.rotation;
        transform.rotation = _startRotation;
        yCoordinate = transform.position.y;


        startRayCastPosition = transform.position;
        targetRayCastPosition = new Vector3(_target.position.x, yCoordinate, _target.position.z);
        distanceToPlayer = targetRayCastPosition - startRayCastPosition;
        cameraDistance = distanceToPlayer.magnitude;

}

    private void Update()
    {
        _startRotation = _target.rotation;
        _startRotation2 = transform.rotation;

        var currentPlayerPosition = new Vector3(_target.position.x, yCoordinate, _target.position.z);
        transform.LookAt(currentPlayerPosition);
        
        
        var deltaRotation = Mathf.Round(transform.transform.eulerAngles.y - _target.transform.eulerAngles.y);

        var rotationDirection = deltaRotation / Mathf.Abs(deltaRotation);

        float deltaAngle = Quaternion.Angle(_target.rotation, transform.rotation);
        RotateCameraFollowTarget(rotationDirection, deltaAngle);
        

        distanceToPlayer = currentPlayerPosition - transform.position;

        if (Mathf.Round(distanceToPlayer.magnitude) > Mathf.Round(cameraDistance))
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
        }
        else if (distanceToPlayer.magnitude < cameraDistance)
        {
            transform.Translate(Vector3.back * _speed * Time.deltaTime, Space.Self);
        }
    }

    private void FixedUpdate()
    {
        var color = Color.red;
        RaycastHit hit;         

        var rayCast = Physics.Raycast(transform.position, distanceToPlayer,out hit , cameraDistance, _mask);

        if (rayCast)
        {
            
        }

        Debug.DrawRay(transform.position, distanceToPlayer, color);
    }

    #endregion


    #region Methods

    private void RotateCameraFollowTarget(float rotation, float angle)
    {
        {
            if (rotation < 0)
            {
                transform.RotateAround(_target.position, Vector3.up, Time.deltaTime * _rotationSpeed * angle);
            }
            else if (rotation > 0)
            {
                transform.RotateAround(_target.position, Vector3.up, Time.deltaTime * _rotationSpeed * -1 * angle);
            }
        }
    }

    #endregion

}
