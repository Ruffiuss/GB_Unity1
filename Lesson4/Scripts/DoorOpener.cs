using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject[] targetDoor;
    [SerializeField] private GameObject targetLamp;

    private MeshRenderer lampMaterials;
    private MeshRenderer leverMaterials;
    private bool isPlayerInArea = false;

    #endregion


    #region UnityMethods

    private void Start()
    {
        lampMaterials = targetLamp.GetComponent<MeshRenderer>();
        lampMaterials.material = lampMaterials.materials[0];

        leverMaterials = gameObject.GetComponent<MeshRenderer>();
        leverMaterials.material = leverMaterials.materials[0];
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
                foreach (var item in targetDoor)
                {
                    Destroy(item);
                }
                lampMaterials.material = lampMaterials.materials[1];
                leverMaterials.material = leverMaterials.materials[1];
            }
        }
    }

    #endregion
}
