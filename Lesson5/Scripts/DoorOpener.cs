using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class DoorOpener : MonoBehaviour
    {


        #region Fields

        [SerializeField] private GameObject[] _targetDoor;
        [SerializeField] private GameObject _targetLamp;

        private MeshRenderer _lampMaterials;
        private MeshRenderer _leverMaterials;

        private bool isPlayerInArea = false;

        #endregion


        #region UnityMethods

        private void Start()
        {

            _lampMaterials = _targetLamp.GetComponent<MeshRenderer>();
            _lampMaterials.material = _lampMaterials.materials[0];

            _leverMaterials = gameObject.GetComponent<MeshRenderer>();
            _leverMaterials.material = _leverMaterials.materials[0];

        }

        private void Update()
        {
            LeverActivate();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isPlayerInArea = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                isPlayerInArea = false;
            }
        }

        #endregion


        #region Methods

        private void LeverActivate()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (isPlayerInArea)
                {
                    foreach (var item in _targetDoor)
                    {
                        Destroy(item);
                    }
                    transform.gameObject.SetActive(false);


                    _lampMaterials.material = _lampMaterials.materials[1];
                    _leverMaterials.material = _leverMaterials.materials[1];
                }
            }
        }

        #endregion


    }


}

