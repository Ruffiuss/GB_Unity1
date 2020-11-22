using UnityEngine;


namespace HomeworksUnityLevel1
{


    public class CollisionChecker1 : MonoBehaviour
    {


        #region UnityMethods

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.CompareTag("Enemy"))
            {
                var parentObject = collider.transform.parent.gameObject;
                Destroy(parentObject);
                Destroy(gameObject);
            }
        }

        #endregion


    }


}
