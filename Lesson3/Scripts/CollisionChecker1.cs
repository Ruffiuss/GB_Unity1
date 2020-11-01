using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker1 : MonoBehaviour
{

    #region UnityMethods

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }

    #endregion
}
