using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


namespace HomeworksUnityLevel1
{

    public class SceneLoader : MonoBehaviour
    {


        #region Fields

        [SerializeField] private Canvas _menu;
        [SerializeField] private Canvas _qualitySettings;
        [SerializeField] private GameObject _audioSettings;
        [SerializeField] private AudioMixer _audioMixer;

        private Button _audioSettingsButton;
        private Slider _audioSettingsSlider;
        private Text _qualityIdentifier;
        private Dropdown _levelRDropdown;

        private int _sceneIndex;
        private int _currenQualityLevel;
        private bool _isActivated = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _qualitySettings.enabled = false;
            _qualityIdentifier = _qualitySettings.GetComponentInChildren<Text>();

            _levelRDropdown = _menu.GetComponentInChildren<Dropdown>();
            _sceneIndex = _levelRDropdown.value + 1;

            _audioSettingsButton = _audioSettings.GetComponent<Button>();
            _audioSettingsSlider = _audioSettings.GetComponentInChildren<Slider>();
            _audioSettingsSlider.gameObject.SetActive(false);
            _audioMixer.GetFloat("masterVolume", out var volume);
            _audioSettingsSlider.value = volume;
        }

        private void Update()
        {
            _currenQualityLevel = QualitySettings.GetQualityLevel();
            _qualityIdentifier.text = $"Current quality level: {_currenQualityLevel}";
        }

        #endregion


        #region Methods

        public void LoadLevel()
        {
            SceneManager.LoadScene(_sceneIndex);
            Time.timeScale = 1;
        }

        public void ChangeSettings()
        {
            _menu.enabled = false;
            _qualitySettings.enabled = true;
            _currenQualityLevel = QualitySettings.GetQualityLevel();
            _qualityIdentifier.text = $"Current quality level: {_currenQualityLevel+1}";
        }

        public void ExitSettings()
        {
            _menu.enabled = true;
            _qualitySettings.enabled = false;
        }

        public void SetQualityLevel(int value)
        {
            QualitySettings.SetQualityLevel(value, true);
        }

        public void LoadLevelIndexUpdate()
        {
            _sceneIndex = _levelRDropdown.value + 1;
        }

        public void SoundSettings()
        {
            if (!_isActivated)
            {
                _audioSettingsSlider.gameObject.SetActive(true);
                _isActivated = true;
            }
            else
            {
                _audioSettingsSlider.gameObject.SetActive(false);
                _isActivated = false;
            }
        }

        public void ChangeVolume()
        {
            if (_audioSettingsSlider.value > -21.0f)
            {
                _audioMixer.SetFloat("masterVolume", _audioSettingsSlider.value);
            }
            else
            {

                _audioMixer.SetFloat("masterVolume", -80.0f);
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion

    }


}
