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
            _audioSettingsSlider.gameObject.SetActive(true);
        }

        public void ChangeVolume()
        {
            //todo
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion

    }


}
