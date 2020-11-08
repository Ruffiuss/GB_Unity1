using UnityEngine;

namespace HomeworksUnityLevel1
{


    public class Turret : MonoBehaviour
    {


        #region Fields

        [SerializeField] private Bullets _bullet;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _startBullet;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private float _reloadTime = 1000.0f;

        private bool isPlayerInRange = false;
        private bool isReloading = false;

        #endregion


        #region Unity Methods

        private void Start()
        {
        }

        private void Update()
        {

            if (isPlayerInRange)
            {
                
            }
        }

        private void FixedUpdate()
        {
            RaycastHit hit;

            var color = Color.red;

            var currentPositon = transform.position;
            currentPositon.y += 0.5f;
            var targetPosition = _target.position;
            targetPosition.y += 0.5f;

            var directionToTarget = targetPosition - currentPositon;

            var rayCast = Physics.Raycast(currentPositon, directionToTarget, out hit, directionToTarget.magnitude, _mask);

            transform.LookAt(directionToTarget);

            if (rayCast)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Attack(directionToTarget);
                    color = Color.green;
                }
            }
            Debug.DrawRay(currentPositon, directionToTarget, color);
        }

        #endregion


        #region Methods

        private void Attack(Vector3 target)
        {

            if (!isReloading)
            {
                Bullets bullet = Instantiate(_bullet, _startBullet.position, _startBullet.rotation);
                bullet.LaunchBullet(target);
                isReloading = true;
                Invoke(nameof(Reload), _reloadTime);
            }
        }
        private void Reload()
        {
            isReloading = false;
        }

        #endregion


    }


}