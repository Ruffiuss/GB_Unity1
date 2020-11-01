using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _lifeTime = 15.0f;

    #endregion

    #region UnityMethods

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    #endregion
}
