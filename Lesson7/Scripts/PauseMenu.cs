using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace HomeworksUnityLevel1
{

    public class PauseMenu : MonoBehaviour
    {


        #region Fields

        [SerializeField] private Canvas _pauseMenu;
        private bool isPaused = false;

        #endregion


        #region UnityUpdate

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    _pauseMenu.enabled = true;
                    Time.timeScale = 0;
                    isPaused = true;
                }
                else
                {
                    _pauseMenu.enabled = false;
                    Time.timeScale = 1;
                    isPaused = false;
                }
            }
        }

        #endregion


        #region Methods

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        #endregion

    }


}

