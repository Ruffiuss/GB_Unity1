using UnityEngine;
using UnityEngine.UI;


namespace HomeworksUnityLevel1
{ 


    public class PlayerTips : MonoBehaviour
    {

        #region Fields

        [SerializeField] private GameObject[] _tips;
        [SerializeField] private GameObject _playerListener;

        private LemonController _lemonController;
        private Canvas _mainCanvas;
        private GameObject _activate;
        private Image _ammo1;
        private Image _ammo2;
        private Image _healthBar;
        private Text _ammo1Count;
        private Text _ammo2Count;

        private float _maxHealth;
        private float _currentHealth;

        public bool Activate
        {
            set { _activate.SetActive(value); }
        }

        #endregion


        #region UnityMEthods

        private void Start()
        {
            _mainCanvas = GetComponent<Canvas>();
            _mainCanvas.enabled = true;

            _lemonController = _playerListener.GetComponent<LemonController>();

            SetTipsControls(_tips);

            _maxHealth = _lemonController.CheckHealth();

        }

        private void Update()
        {
            _ammo1Count.text = _lemonController.TrapCount.ToString();
            _ammo2Count.text = _lemonController.BaitCount.ToString();

            _currentHealth = _lemonController.CheckHealth();
            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }

        #endregion


        #region Methods

        private void SetTipsControls(GameObject[] objects)
        {
            foreach (var item in objects)
            {
                if (item.name.Equals("Activate"))
                {
                    _activate = item;
                    _activate.SetActive(false);
                }
                if (item.name.Equals("Ammo1"))
                {
                    _ammo1 = item.GetComponent<Image>();
                    _ammo1.enabled = true;
                }
                if (item.name.Equals("Ammo2"))
                {
                    _ammo2 = item.GetComponent<Image>();
                    _ammo2.enabled = true;
                }
                if (item.name.Equals("Ammo1"))
                {
                    _ammo1Count = item.GetComponentInChildren<Text>();
                }
                if (item.name.Equals("Ammo2"))
                {
                    _ammo2Count = item.GetComponentInChildren<Text>();
                }
                if (item.name.Equals("HealthBar"))
                {
                    _healthBar = item.GetComponent<Image>();
                }
            }
            
            
        }

        #endregion

    }


}
