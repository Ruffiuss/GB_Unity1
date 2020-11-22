using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class ParticlesController : MonoBehaviour
    {

        #region Fields

        private ParticleSystem _particles;

        #endregion


        #region Properties

        public ParticleSystem Particles => _particles;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _particles = GetComponentInChildren<ParticleSystem>();            
        }

        #endregion


        #region Methods

        public void MakeParticles()
        {
            _particles.Play();
        }

        public void RemoveParticles()
        {
            _particles.Stop();
        }

        #endregion

    }


}
