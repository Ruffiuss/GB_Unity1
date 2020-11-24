using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class AudioSourceChanger : MonoBehaviour
    {

        #region Fields

        [SerializeField] private AudioClip[] _sounds;

        private AudioSource _source;

        #endregion


        #region Properties

        public int Sounds
        {
            get { return _sounds.Length; }
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _source.clip = _sounds[0];
        }

        #endregion


        #region Methods

        public void PlayHitSound(int value)
        {
            _source.PlayOneShot(_sounds[value]);
        }

        public void ChangePitch(float value)
        {
            _source.pitch = value;
        }

        public AudioClip GetAudioClip(int value)
        {
            return _sounds[value];
        }

        public void SetAudioClip(int value)
        {
            _source.clip = _sounds[value];
        }

        #endregion


    }



}


