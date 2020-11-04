using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker1 : MonoBehaviour
{

    #region UnityMethods

    private void OnTriggerEnter(Collider collider)
    {
        //var currentChildren = collider.gameObject.transform.GetChild(0);
        if(collider.gameObject.CompareTag("Enemy"))
        {
            var parentObject = collider.transform.parent.gameObject;
            Destroy(parentObject);
            Destroy(gameObject);
        }
    }

    #endregion
}
