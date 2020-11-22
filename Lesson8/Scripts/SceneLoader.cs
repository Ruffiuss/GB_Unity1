using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace HomeworksUnityLevel1
{

    public class SceneLoader : MonoBehaviour
    {


        #region Fields

        [SerializeField] private Canvas _menu;
        [SerializeField] private Canvas _settings;

        private Text _qualityIdentifier;
        private Dropdown _levelRDropdown;

        private int _sceneIndex;
        private int _currenQualityLevel;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _settings.enabled = false;
            _qualityIdentifier = _settings.GetComponentInChildren<Text>();

            _levelRDropdown = _menu.GetComponentInChildren<Dropdown>();
            _sceneIndex = _levelRDropdown.value + 1;
        }

        private void Update()
        {
            _currenQualityLevel = QualitySettings.GetQualityLevel();
            _qualityIdentifier.text = $"Current quality level: {_currenQualityLevel}";

            Debug.Log(_sceneIndex);
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
            _settings.enabled = true;
            _currenQualityLevel = QualitySettings.GetQualityLevel();
            _qualityIdentifier.text = $"Current quality level: {_currenQualityLevel+1}";
        }

        public void ExitSettings()
        {
            _menu.enabled = true;
            _settings.enabled = false;
        }

        public void SetQualityLevel(int value)
        {
            QualitySettings.SetQualityLevel(value, true);
        }

        public void LoadLevelIndexUpdate()
        {
            _sceneIndex = _levelRDropdown.value + 1;
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion

    }


}
