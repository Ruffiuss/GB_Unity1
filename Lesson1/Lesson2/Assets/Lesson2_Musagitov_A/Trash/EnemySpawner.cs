using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    private GameObject[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enem");
        foreach (var item in Enemies)
        {
            Instantiate(_enemy, item.transform.position, item.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
