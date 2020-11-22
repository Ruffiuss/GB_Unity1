using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class Bullets : MonoBehaviour
    {


        #region Fields

        [SerializeField] private float _bulletSpeed;

        private Rigidbody _rigidbody;

        private float _lifeTime = 3.0f;

        #endregion


        #region Methods

        public void LaunchBullet(Vector3 target)
        {
            Destroy(gameObject, _lifeTime);

            _rigidbody = GetComponent<Rigidbody>();

            Vector3 impulse = target * _rigidbody.mass * _bulletSpeed;
            _rigidbody.AddForce(impulse, ForceMode.Impulse);
        }

        #endregion


    }


}
